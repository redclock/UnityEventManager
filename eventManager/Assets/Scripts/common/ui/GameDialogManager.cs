using UnityEngine;
using System.Collections.Generic;

public class GameDialogManager: GameCompBase {
	private List<GameDialogBase> _dialogs= new List<GameDialogBase>();

	void Awake() {
		DontDestroyOnLoad = true;	
	}

	private void resetOrder(int index)
	{
		_dialogs[index].transform.SetSiblingIndex(index);
	}

	private void resetOrder()
	{
		for (int i = 0; i < _dialogs.Count; i++)
		{
			resetOrder(i);
		}
	}

	private void removeDialogAt(int index) {
		if (index >= 0 && index < _dialogs.Count) {
			var dialog = _dialogs[index];
			_dialogs.RemoveAt (index);
			dialog.onPop ();
		}
	}

	public int findDialogIndex(GameDialogBase dialog)
	{
		return _dialogs.IndexOf(dialog);
	}
	
	public bool isDialogShow(GameDialogBase dialog)
	{
		return findDialogIndex(dialog) >= 0;
	}
	
	public GameDialogBase getTop()
	{
		return _dialogs.Count == 0 ? null : _dialogs [_dialogs.Count - 1];
	}
	
	public GameDialogBase popTop()
	{
		if (_dialogs.Count == 0) 
		{
			return null;
		} 
		else 
		{
			var dialog = _dialogs[_dialogs.Count - 1];
			_dialogs.RemoveAt(_dialogs.Count - 1);
			dialog.onPop();
			Debug.Log ("Pop Top: " + dialog.gameObject.name);
			if (_dialogs.Count > 0)
			{
				var top = getTop();
				top.onBecomeTop();
			}
			
			resetOrder();
			return dialog;
		}
	}
	
	public bool popDialog(GameDialogBase dialog)
	{
		if (dialog == null)
			return false;
		
		int index = findDialogIndex (dialog);
		if (index < 0)
		{
			return false;
		}
		else if (index == _dialogs.Count - 1)
		{
			popTop();
		}
		else
		{
			_dialogs.RemoveAt(index);
			dialog.onPop();
			resetOrder();
		}
		return true;
	}

	GameDialogBase loadFromPrefab(string prefabName) {
		var resObject = Resources.Load (prefabName);
		if (resObject == null)
			return null;
		var dialogObject = Instantiate(resObject) as GameObject;
		
		var dialogComponent = dialogObject.GetComponent<GameDialogBase> ();
		return dialogComponent;
	}

	public void pushDialog(string prefabName)
	{
		Debug.Log ("Push: " + prefabName);

		var dialog = loadFromPrefab (prefabName);
		Debug.Assert (dialog != null);
		dialog.setDialogManager (this);
		dialog.transform.parent = transform;

		var top = getTop ();
		if (top != null)
			top.onLoseTop ();
		_dialogs.Add (dialog);

		dialog.onPush ();
		resetOrder(_dialogs.Count - 1);
	}
	
	public void popAll()
	{
		int count = _dialogs.Count;
		for (int i = 0; i < count; i++)
		{
			popTop();
		}
		resetOrder ();
	}
	
	public int getDialogDepth() {
		return _dialogs.Count;
	}
	
	public void onBackKey() {
		var topDialog = getTop ();
		if (topDialog != null)
		{
			topDialog.onBackKey();
		}
	}

}
