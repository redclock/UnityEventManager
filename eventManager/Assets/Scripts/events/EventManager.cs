using System.Collections.Generic;

public class EventManager {
	private class EventRecord {
		public System.Type eventType;
		public bool isDispactching = false;
		public List<EventListener> listeners = new List<EventListener>();

		public EventRecord(System.Type eventType) {
			this.eventType = eventType;
		}

		public void addListener(EventListener listener) {
			listeners.Add (listener);
		}

		public void dispatch(GameEvent gameEvent) {
			isDispactching = true;
			for (int i = listeners.Count; i >= 0; i--) {
				var listener = listeners[i];
				if (!listener.isDeleted()) {
					listener.callDelegate(gameEvent);
				}
			}
			isDispactching = false;

			for (int i = listeners.Count; i >= 0; i--) {
				var listener = listeners [i];
				if (listener.isDeleted ()) {
					listeners.RemoveAt(i);
				}
			}
		}

		public void removeListener(EventListener listener) {
			listener.markDeleted ();
		}

		public void clear() {
			for (int i = listeners.Count; i >= 0; i--) {
				listeners [i].markDeleted();
			}
			listeners.Clear ();
		}
	}

	private Dictionary<System.Type, EventRecord> _records = new Dictionary<System.Type, EventRecord>();

	private EventRecord getEventRecord(System.Type eventType) {
		EventRecord record;
		if (!_records.TryGetValue (eventType, out record)) {
			record = new EventRecord(eventType);
			_records.Add(eventType, record);
		}
		return record;
	}

	public EventListener listen<T>(EventListener<T>.EventDelegate callback) where T : GameEvent {
		EventListener listener = new EventListener<T> (callback);
		getEventRecord (typeof(T)).addListener (listener);
		return listener;
	}

	public void removeListener<T>(EventListener listener) {
		getEventRecord (listener.eventType).removeListener (listener);
	}

	public void send(GameEvent gameEvent) {
		getEventRecord (gameEvent.GetType ()).dispatch (gameEvent);
	}

	public void removeEventListeners(GameEvent gameEvent) {
		getEventRecord (gameEvent.GetType ()).clear ();
	}
}
