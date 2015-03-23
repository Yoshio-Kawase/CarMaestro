using UnityEngine;
using System.Collections;

public class counterRight : MonoBehaviour
{

	void OnTriggerEnter2D(Collider2D col){
		GameObject targetRight = GameObject.Find ("spawner_right");
		Spawner compRight = targetRight.GetComponent<Spawner> ();
		compRight.carCount--;
	}

}

