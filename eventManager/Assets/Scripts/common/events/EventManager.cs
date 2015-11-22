using UnityEngine;
using System.Collections.Generic;

public class EventManager {

	private Dictionary<System.Type, EventDispatcher> _dispatchers = new Dictionary<System.Type, EventDispatcher>();

	public EventListener listen<T>(EventListenerForEvent<T>.EventDelegate callback) where T : GameEvent {
		if (callback == null)
			return null;
		// create listener
		EventListener listener = new EventListenerForEvent<T> (callback);
		// find proper dispatcher
		EventDispatcher disp;
		if (!_dispatchers.TryGetValue (listener.eventType, out disp)) {
			disp = new NormalEventDispatcher(listener.eventType);
			_dispatchers.Add(listener.eventType, disp);
		}
		disp.addListener (listener);

		// send observe event
		GameListenEvent<T> gameListenEvent = new GameListenEvent<T> (listener);
		send (gameListenEvent);

		return listener;
	}

	public EventListener observeListen<T>(EventListenerObserve<T>.EventDelegate callback) where T : GameEvent {
		if (callback == null)
			return null;
		EventListener listener = new EventListenerObserve<T> (callback);
		EventDispatcher dispatcher;
		if (!_dispatchers.TryGetValue (listener.eventType, out dispatcher)) {
			dispatcher = new ObserveEventDispatcher(listener.eventType, this);
			_dispatchers.Add(listener.eventType, dispatcher);
		}
		dispatcher.addListener (listener);

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
		if (_dispatchers.TryGetValue(listener.eventType, out rec)) {
			rec.removeListener (listener);
		}
	}

	public void send(GameEvent gameEvent) {
		if (gameEvent == null)
			return;
		Debug.Log ("send event: " + gameEvent.GetType ().ToString ());
		EventDispatcher rec;
		if (_dispatchers.TryGetValue(gameEvent.GetType (), out rec)) {
			rec.dispatch (gameEvent);
		}
	}

	public void removeAllListenersForEvent(GameEvent gameEvent) {
		if (gameEvent == null)
			return;
		EventDispatcher rec;
		if (_dispatchers.TryGetValue(gameEvent.GetType (), out rec)) {
			rec.clear();
		}
	}
}
