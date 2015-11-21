using UnityEngine;

public class GameCompBase : MonoBehaviour {
	
	private GameEventComponent _eventComp;
	protected GameEventComponent eventComp {
		get {
			if (_eventComp == null)
				_eventComp = GameEventComponent.Create(gameObject);
			return _eventComp;
		}
	}
}
