using UnityEngine;
using System.Collections.Generic;

public class GameEventComponent : MonoBehaviour {
	private List<EventListener> _listeners = new List<EventListener>();
	private static EventManager _eventManager = new EventManager ();

	public static GameEventComponent Create(GameObject gameObject) {
		GameEventComponent comp = gameObject.GetComponent<GameEventComponent> ();
		if (comp == null) {
			comp = gameObject.AddComponent<GameEventComponent>();
		}
		return comp;
	}

	public EventListener listen<T>(EventListener<T>.EventDelegate callback) where T: GameEvent {
		var listener = _eventManager.listen<T> (callback);
		_listeners.Add (listener);
		return listener;
	}

	public void removeListener(EventListener listener) {
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
		_eventManager.send (gameEvent);	
	}

	// Remove all listeners when the componet destroyed
	void OnDestroy() {
		Debug.Log("destroy listeners");
		clearListeners ();
	}
}
