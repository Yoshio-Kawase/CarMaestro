using UnityEngine;
using System.Collections;

public class GuageLeft : MonoBehaviour {
	public float value = 0;
	public float maxValue = 10f;
	public float changeSpeed = 1.0f;
	
	// Update is called once per frame
	void Update () {
		GameObject targetLeft = GameObject.Find ("spawner_left");
		Spawner compLeft = targetLeft.GetComponent<Spawner> ();

		value = Mathf.PingPong(compLeft.carCount, maxValue);
		
		if (compLeft.carCount == 10) {
			Application.LoadLevel ("end");
		}
	}


}
