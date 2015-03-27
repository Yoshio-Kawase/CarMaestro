using UnityEngine;
using System.Collections;

public class SwitchScene2 : MonoBehaviour
{
	private int x = 0;

	void Update() {
		if (x > 10) {
			Application.LoadLevel ("main");
		}
		x++;		
	}
}

