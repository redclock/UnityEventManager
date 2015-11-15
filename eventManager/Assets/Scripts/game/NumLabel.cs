using UnityEngine;
using System.Collections;

public class NumLabel : MonoBehaviour {
	GameEventComponent _eventComp;
	int num = 0;
	// Use this for initialization
	void Start () {
		updateNum ();
		_eventComp = GameEventComponent.Create (gameObject);
		_eventComp.listen ((GameEventAddNum ev) => {
			num += ev.AddNum;
			updateNum();
		});
	}

	void updateNum() {
		var textMesh = GetComponent<TextMesh> ();
		textMesh.text = num.ToString ();
	}

	void OnGUI() {
		GUI.Label (new Rect (0, 100, 100, 20), num.ToString ()); 
	}
}
