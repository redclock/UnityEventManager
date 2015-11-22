using UnityEngine;
using System.Collections.Generic;

abstract class EventDispatcher {
	public System.Type eventType;
	public int dispactchingLevel = 0;
	public List<EventListener> listeners = new List<EventListener>();
	
	public EventDispatcher(System.Type eventType) {
		this.eventType = eventType;
	}
	
	public void addListener(EventListener listener) {
		Debug.Log ("add listener: " + eventType.ToString ());
		listeners.Add (listener);
	}
	
	protected abstract void handleListener (GameEvent gameEvent, EventListener listener);
	
	public void dispatch(GameEvent gameEvent) {
		dispactchingLevel++;
		for (int i = listeners.Count - 1; i >= 0; i--) {
			var listener = listeners[i];
			if (!listener.isDeleted()) {
				Debug.Log("trigger listener: " + eventType.ToString());
				handleListener(gameEvent, listener);
			}
		}
		dispactchingLevel--;
		
		for (int i = listeners.Count - 1; i >= 0; i--) {
			var listener = listeners [i];
			if (listener.isDeleted ()) {
				Debug.Log ("remove listener: " + eventType.ToString ());
				listeners.RemoveAt(i);
			}
		}
	}
	
	public void removeListener(EventListener listener) {
		if (dispactchingLevel > 0) {
			listener.markDeleted ();
		} else {
			listeners.Remove(listener);
		}
	}
	
	public void clear() {
		for (int i = listeners.Count - 1; i >= 0; i--) {
			listeners [i].markDeleted();
		}
		listeners.Clear ();
	}
}

class NormalEventDispatcher: EventDispatcher {
	public NormalEventDispatcher(System.Type eventType): base(eventType) {
		
	}
	
	protected override void handleListener (GameEvent gameEvent, EventListener listener) {
		listener.callDelegate (gameEvent);
	}
}

class ObserveEventDispatcher: EventDispatcher {
	private EventManager _eventManager;
	public ObserveEventDispatcher(System.Type eventType, EventManager eventManager): base(eventType) {
		_eventManager = eventManager;
	}
	
	protected override void handleListener (GameEvent gameEvent, EventListener listener) {
		var eventObserve = (GameListenEvent)gameEvent;
		GameEvent genEvent = listener.getEvent();
		if (eventObserve.listener == null) {
			_eventManager.send (genEvent);
		} else {
			eventObserve.listener.callDelegate(genEvent);
		}
	}
}

