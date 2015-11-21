using UnityEngine;
using System.Collections.Generic;

public class GameEventComponent : MonoBehaviour {
	public bool isEnabled = true;

	private List<EventListener> _listeners = new List<EventListener>();
	private static EventManager _eventManager = new EventManager ();

	public static GameEventComponent Create(GameObject gameObject) {
		GameEventComponent comp = gameObject.GetComponent<GameEventComponent> ();
		if (comp == null) {
			comp = gameObject.AddComponent<GameEventComponent>();
		}
		return comp;
	}

	public EventListener listen<T>(EventListenerForEvent<T>.EventDelegate callback) where T: GameEvent {
		if (!isEnabled)
			return null;

		var listener = _eventManager.listen<T> (callback);
		_listeners.Add (listener);
		return listener;
	}

	public EventListener observeListen<T>(EventListenerObserve<T>.EventDelegate callback) where T: GameEvent {
		if (!isEnabled)
			return null;
		var listener = _eventManager.observeListen<T> (callback);
		_listeners.Add (listener);
		return listener;
	}

	public void removeListener(EventListener listener) {
		if (!isEnabled)
			return;
		_eventManager.removeListener (listener);
		int index = _listeners.IndexOf (listener);
		if (index >= 0) {
			_listeners.RemoveAt(index);
		}
	}

	public void clearListeners() {
		for (int i = 0; i < _listeners.Count; i++) {
			_eventManager.removeListener(_listeners[i]);
		}
		_listeners.Clear ();
	}

	public void send(GameEvent gameEvent) {
		if (!isEnabled)
			return;
		_eventManager.send (gameEvent);	
	}

	// Remove all listeners when the componet destroyed
	void OnDestroy() {
		Debug.Log("destroy listeners");
		clearListeners ();
	}
}
