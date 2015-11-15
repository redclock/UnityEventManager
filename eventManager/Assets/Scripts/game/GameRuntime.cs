using UnityEngine;
using System.Collections;

public class GameRuntime : MonoBehaviour {
	GameEventComponent _eventComp;

	// Use this for initialization
	void Start () {
		_eventComp = GameEventComponent.Create (gameObject);

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			_eventComp.send(new GameEventAddNum(1));
		}
	}

	void OnGUI() {
		GUI.Button (new Rect (0, 0, 100, 100), "Add");
	}
}
