using UnityEngine;
using System.Collections;

public class GameRuntime : GameRootBase {
	void InitializeLogic() {
		gameObject.AddComponent<GameLogic> ();
	}

	void Start () {
		InitializeLogic ();
	}
	
	// Update is called once per frame
	void Update () {
	}
}
