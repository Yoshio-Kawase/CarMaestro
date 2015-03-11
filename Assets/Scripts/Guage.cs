using UnityEngine;
using System.Collections;

public class Guage : MonoBehaviour {
	public float value = 0;
	public float maxValue = 10f;
	public float changeSpeed = 1.0f;
	
	// Update is called once per frame
	void Update () {
        Spawner target = GameObject.Find("spawner").GetComponent<Spawner>();
		//print (target.carCount);
		value = Mathf.PingPong(Time.time * changeSpeed, maxValue);
	}
}
