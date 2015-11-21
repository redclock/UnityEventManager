using UnityEngine;
using System.Collections;

public class GameLogicBase : GameCompBase {
	protected virtual void registerEvents() {
		
	}
	
	// Use this for initialization
	protected virtual void Awake () {
		registerEvents ();
	}

}
