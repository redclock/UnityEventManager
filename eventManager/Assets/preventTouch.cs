using UnityEngine;
using System.Collections;

public class preventTouch : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void onClick(UnityEngine.EventSystems.BaseEventData data) {
		Debug.Log("ON CLICK");
		data.Use ();
	}
}
