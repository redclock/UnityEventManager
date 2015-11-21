using UnityEngine;
using System.Collections.Generic;

public class EventManager {

	private Dictionary<System.Type, EventDispatcher> _records = new Dictionary<System.Type, EventDispatcher>();

	public EventListener listen<T>(EventListenerForEvent<T>.EventDelegate callback) where T : GameEvent {
		if (callback == null)
			return null;
		EventListener listener = new EventListenerForEvent<T> (callback);
		EventDispatcher rec;
		if (!_records.TryGetValue (listener.eventType, out rec)) {
			rec = new NormalEventDispatcher(listener.eventType);
			_records.Add(listener.eventType, rec);
		}
		rec.addListener (listener);
		return listener;
	}

	public EventListener observeListen<T>(EventListenerObserve<T>.EventDelegate callback) where T : GameEvent {
		if (callback == null)
			return null;
		EventListener listener = new EventListenerObserve<T> (callback);
		EventDispatcher rec;
		if (!_records.TryGetValue (listener.eventType, out rec)) {
			rec = new NormalEventDispatcher(listener.eventType);
			_records.Add(listener.eventType, rec);
		}
		rec.addListener (listener);

		GameEvent genEvent = listener.getEvent();
		if (genEvent != null) {
			send (genEvent);
		}

		return listener;
	}


	public void removeListener(EventListener listener) {
		if (listener == null)
			return;
		EventDispatcher rec;
		if (_records.TryGetValue(listener.eventType, out rec)) {
			rec.removeListener (listener);
		}
	}

	public void send(GameEvent gameEvent) {
		if (gameEvent == null)
			return;
		Debug.Log ("send event: " + gameEvent.GetType ().ToString ());
		EventDispatcher rec;
		if (_records.TryGetValue(gameEvent.GetType (), out rec)) {
			rec.dispatch (gameEvent);
		}
	}

	public void removeAllListenersForEvent(GameEvent gameEvent) {
		if (gameEvent == null)
			return;
		EventDispatcher rec;
		if (_records.TryGetValue(gameEvent.GetType (), out rec)) {
			rec.clear();
		}
	}
}
