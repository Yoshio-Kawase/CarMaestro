using UnityEngine;
using System.Collections;

public class GuageBottom : MonoBehaviour {
	public float value = 0;
	public float maxValue = 10f;
	public float changeSpeed = 1.0f;
	
	// Update is called once per frame
	void Update () {
		GameObject targetBottom = GameObject.Find ("spawner_bottom");
		Spawner compBottom = targetBottom.GetComponent<Spawner> ();
		
		value = Mathf.PingPong(compBottom.carCount, maxValue);
		
		if (compBottom.carCount == 10) {
			Application.LoadLevel ("end");
		}
	}


}
