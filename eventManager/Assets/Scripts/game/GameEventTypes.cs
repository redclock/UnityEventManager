
namespace gameEvents {
	public class AddNum: GameEvent {
		public int addNum;
		public AddNum(int num) {
			addNum = num;
		}
	}
	
	public class NumChanged: GameEvent {
		public int num;
		public NumChanged(int num) {
			this.num = num;
		}
	}
}

