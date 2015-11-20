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
			for (int i = listeners.Count - 1; i >= 0; i--) {
				var listener = listeners[i];
				if (!listener.isDeleted()) {
					listener.callDelegate(gameEvent);
				}
			}
			isDispactching = false;

			for (int i = listeners.Count - 1; i >= 0; i--) {
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
			for (int i = listeners.Count - 1; i >= 0; i--) {
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
		if (callback == null)
			return null;
		EventListener listener = new EventListener<T> (callback);
		EventRecord rec;
		if (!_records.TryGetValue (listener.eventType, out rec)) {
			rec = new EventRecord(listener.eventType);
			_records.Add(listener.eventType, rec);
		}
		rec.addListener (listener);
		return listener;
	}

	public void removeListener(EventListener listener) {
		if (listener == null)
			return;
		EventRecord rec;
		if (_records.TryGetValue(listener.eventType, out rec)) {
			rec.removeListener (listener);
		}
	}

	public void send(GameEvent gameEvent) {
		if (gameEvent == null)
			return;
		EventRecord rec;
		if (_records.TryGetValue(gameEvent.GetType (), out rec)) {
			rec.dispatch (gameEvent);
		}
	}

	public void removeAllListenersForEvent(GameEvent gameEvent) {
		if (gameEvent == null)
			return;
		EventRecord rec;
		if (_records.TryGetValue(gameEvent.GetType (), out rec)) {
			rec.clear();
		}
	}
}
