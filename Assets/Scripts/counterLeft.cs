using UnityEngine;
using System.Collections;

public class counterLeft : MonoBehaviour
{
	public GameObject tarL;
	public Spawner compL;
	public int clearLeft = 0;

	void Start(){
		tarL = GameObject.Find ("spawner_left");
		compL = tarL.GetComponent<Spawner> ();
	}

	void OnTriggerExit2D(Collider2D col){
		if (compL.carCount > 0) {
			compL.carCount--;
			clearLeft++;
		}
	}
}

