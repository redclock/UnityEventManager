
public interface EventListener {
	System.Type eventType { get; }
	void callDelegate(GameEvent e);
	void markDeleted ();
	bool isDeleted ();
}

public class EventListener<T>: EventListener where T: GameEvent {
	public delegate void EventDelegate (T e);
	
	private bool _isDeleted;
	private EventDelegate _delegate;
	
	public System.Type eventType { get { return typeof(T); } }
	
	public void markDeleted() { _isDeleted = true; }
	public bool isDeleted() { return _isDeleted; }
	
	public void callDelegate(GameEvent e) {
		_delegate.Invoke ((T)e);
	}
	
	public EventListener(EventDelegate del) {
		_delegate = del;		
	}
}
