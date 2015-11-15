﻿using UnityEngine;
using System.Collections;

class GameEventMessage: GameEvent {
	public string Message;
	public GameEventMessage(string msg) {
		Message = msg;
	}
}


public class GameRuntime : MonoBehaviour {
	EventManager _eventManager = new EventManager();
	// Use this for initialization
	void Start () {
		_eventManager.listen<GameEventMessage> ((ev) => {
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
	}
}
