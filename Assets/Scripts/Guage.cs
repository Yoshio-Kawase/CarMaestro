using UnityEngine;
using System.Collections;

public class Guage : MonoBehaviour {
	public float value = 0;
	public float maxValue = 10f;
	public float changeSpeed = 1.0f;
	
	// Update is called once per frame
	void Update () {
		GameObject target = GameObject.Find("spawner");
		Spawner sp = target.GetComponent<Spawner> ();
		value = Mathf.PingPong(sp.carCount, maxValue);

		//if (sp.carCount == 3) {
		//	Application.LoadLevel ("end");
		//}
	}


}
