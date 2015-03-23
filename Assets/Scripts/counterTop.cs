using UnityEngine;
using System.Collections;

public class counterTop : MonoBehaviour
{

	void OnTriggerEnter2D(Collider2D col){
		GameObject targetTop = GameObject.Find ("spawner");
		Spawner compTop = targetTop.GetComponent<Spawner> ();
		compTop.carCount--;
	}

}

