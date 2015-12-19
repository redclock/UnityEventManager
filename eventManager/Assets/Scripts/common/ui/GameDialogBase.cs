using UnityEngine;
using System.Collections;

public class GameDialogBase : GameCompBase {

	protected bool isPushed = false;
	protected GameDialogManager manager;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setDialogManager(GameDialogManager manager) {
		this.manager = manager;
	}

	public GameDialogManager getDialogManager() {
		return manager;
	}

	// when push to stack;
	public virtual void onPush() { isPushed = true; appear (); }
	// when pop from stack;
	public virtual void onPop() { isPushed = false; disappear (); }
	// when new dialog pushed as top while this dialog is top before
	public virtual void onLoseTop() {}
	// when top dialog popped while this dialog is the second top before
	public virtual void onBecomeTop() {}
	
	public virtual void onBackKey() {}

	public virtual void setVisible(bool b)
	{
		gameObject.SetActive (b);
	}

	public virtual void appear() 
	{
		setVisible (true);
		Destroy (gameObject);
		//playAnimation("Dialog Appear");
	}
	
	public virtual void disappear() 
	{
		//playAnimation("Dialog Disappear");
		Destroy (gameObject);
	}

}
