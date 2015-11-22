using UnityEngine;
using System.Collections;

public class GameRuntime : GameRootBase {
	void InitializeLogic() {
		gameObject.AddComponent<GameLogic> ();
	}

	void Awake () {
		InitializeLogic ();
	}
	
	// Update is called once per frame
	void Update () {
	}
}
