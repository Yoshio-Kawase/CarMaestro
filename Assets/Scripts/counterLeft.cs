using UnityEngine;
using System.Collections;

public class counterLeft : MonoBehaviour
{

	void OnTriggerEnter2D(Collider2D col){
		GameObject targetLeft = GameObject.Find ("spawner_left");
		Spawner compLeft = targetLeft.GetComponent<Spawner> ();
		compLeft.carCount--;
	}

}

