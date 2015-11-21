using UnityEngine;
using System.Collections;

public class GameLogic : GameLogicBase {
	int num = 100;

	protected override void registerEvents() {
		var ec = eventComp;

		ec.listen ((gameEvents.AddNum evt) => {
			num += evt.addNum;
			ec.send(new gameEvents.NumChanged(num));
		});

		ec.observeListen (() => new gameEvents.NumChanged (num));

	}
}
