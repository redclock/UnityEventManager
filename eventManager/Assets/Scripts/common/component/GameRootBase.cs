using UnityEngine;
using System.Collections;

public class GameRootBase : MonoBehaviour {

	public static GameObject RootObject;

	protected virtual void Awake() {
		RootObject = gameObject;
	}
}
