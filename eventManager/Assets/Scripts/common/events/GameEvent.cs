
public interface GameEvent {
}

class GameListenEvent: GameEvent{
	public EventListener listener;
	public GameListenEvent(EventListener listener) {
		this.listener = listener;
	}
}

class GameListenEvent<T>: GameListenEvent where T: GameEvent{
	public GameListenEvent(EventListener listener): base(listener) {
	}
}
