using UnityEngine;
using System.Collections;

public class NumLabel : GameCompBase {
	// Use this for initialization
	void Start () {
		eventComp.listen ((gameEvents.NumChanged evt) => {
			GetComponent<UnityEngine.UI.Text>().text = evt.num.ToString();
		});
	}

	void OnGUI() {
		if (GUI.Button (new Rect (0, 0, 100, 100), "Add")) {
			eventComp.send(new gameEvents.AddNum(1));
		}
	}
}
