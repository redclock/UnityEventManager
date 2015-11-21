using UnityEngine;
using System.Collections.Generic;

abstract class EventDispatcher {
	public System.Type eventType;
	public bool isDispactching = false;
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
		isDispactching = true;
		for (int i = listeners.Count - 1; i >= 0; i--) {
			var listener = listeners[i];
			if (!listener.isDeleted()) {
				handleListener(gameEvent, listener);
			}
		}
		isDispactching = false;
		
		for (int i = listeners.Count - 1; i >= 0; i--) {
			var listener = listeners [i];
			if (listener.isDeleted ()) {
				Debug.Log ("remove listener: " + eventType.ToString ());
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
		GameEvent genEvent = listener.getEvent();
		_eventManager.send (genEvent);
	}
}

