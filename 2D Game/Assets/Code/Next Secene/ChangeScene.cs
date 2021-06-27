using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : BeginScene
{
	public Transform player;

	public Transform house2Bottom;

	public Transform house2Front;
	public Transform house1Front;

    // Use this for initialization
	public override void Start () {
		base.Start();

		if (prevScene == 5 || prevScene == 19) {
			player.position = house2Bottom.position;
		}

		if ((prevScene == 4 && currentScene != 5) || (prevScene == 21 && currentScene != 19)) {
			player.position = house2Front.position;
		}

		if (prevScene == 3 || prevScene == 20) {
			player.position = house1Front.position;
		}

	}

}
