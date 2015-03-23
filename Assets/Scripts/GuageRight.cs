using UnityEngine;
using System.Collections;

public class GuageRight : MonoBehaviour {
	public float value = 0;
	public float maxValue = 10f;
	public float changeSpeed = 1.0f;
	
	// Update is called once per frame
	void Update () {
		GameObject targetRight = GameObject.Find ("spawner_right");
		Spawner compRight = targetRight.GetComponent<Spawner> ();

		value = Mathf.PingPong(compRight.carCount, maxValue);

		if (compRight.carCount == 10) {
			Application.LoadLevel ("end");
		}
	}


}
