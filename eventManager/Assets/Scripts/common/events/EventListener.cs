
public abstract class EventListener {
	protected bool _isDeleted;

	public abstract System.Type eventType { get; }

	public virtual void markDeleted() { _isDeleted = true; }
	public virtual bool isDeleted() { return _isDeleted; }
	public virtual void callDelegate(GameEvent e) {}
	public virtual GameEvent getEvent() { return null; }
}

public class EventListenerForEvent<T>: EventListener where T: GameEvent {
	public delegate void EventDelegate (T e);
	
	private EventDelegate _delegate;
	
	public override System.Type eventType { get { return typeof(T); } }

	public override void callDelegate(GameEvent e) {
		_delegate.Invoke ((T)e);
	}

	public EventListenerForEvent(EventDelegate del) {
		_delegate = del;		
	}
}

public class EventListenerObserve<T>: EventListener where T: GameEvent{
	public override System.Type eventType { get { return typeof(GameListenEvent<T>); } }
	public delegate T EventDelegate ();

	private EventDelegate _delegate;
	public override GameEvent getEvent() {
		return _delegate.Invoke();
	}

	public EventListenerObserve(EventDelegate del) {
		_delegate = del;
	}
}
