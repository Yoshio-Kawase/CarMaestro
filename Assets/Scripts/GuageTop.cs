using UnityEngine;
using System.Collections;

public class GuageTop : MonoBehaviour {
	public float value = 0;
	public float maxValue = 10f;
	public float changeSpeed = 1.0f;
	
	// Update is called once per frame
	void Update () {
		GameObject targetTop = GameObject.Find ("spawner");
		Spawner compTop = targetTop.GetComponent<Spawner> ();
		
		value = Mathf.PingPong(compTop.carCount, maxValue);
		
		if (compTop.carCount == 10) {
			Application.LoadLevel ("end");
		}
	}


}
