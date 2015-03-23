using UnityEngine;
using System.Collections;

public class counterBottom : MonoBehaviour
{

	void OnTriggerEnter2D(Collider2D col){
		GameObject targetBottom = GameObject.Find ("spawner_bottom");
		Spawner compBottom = targetBottom.GetComponent<Spawner> ();
		compBottom.carCount--;
	}

}

