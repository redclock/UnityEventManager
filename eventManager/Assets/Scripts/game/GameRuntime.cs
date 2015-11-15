using UnityEngine;
using System.Collections;

class GameEventMessage: GameEvent {
	public string Message;
	public GameEventMessage(string msg) {
		Message = msg;
	}
}


public class GameRuntime : MonoBehaviour {
	EventManager _eventManager = new EventManager();
	EventListener listener = null;
	GameEventComponent _eventComp;

	// Use this for initialization
	void Start () {
		_eventComp = GameEventComponent.Create (gameObject, _eventManager);
		_eventComp.listen ((GameEventMessage ev) => {
			Debug.Log ("receive: " + ev.Message);
			_eventManager.listen<GameEventMessage>((ev2) => {
				Debug.Log ("inner receive: " + ev2.Message);
			});
		});
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			_eventManager.send(new GameEventMessage("Mouse down!"));
		}

		if (Input.GetMouseButtonDown (1)) {
			if (listener != null) {
				_eventManager.removeListener(listener);
				listener = null;
			}
		}
	}
}
