using UnityEngine;
using System.Collections;

public class counterBottom : MonoBehaviour
{

	public GameObject tarB;
	public Spawner compB;
	
	void Start(){
		tarB = GameObject.Find ("spawner_bottom");
		compB = tarB.GetComponent<Spawner> ();
	}
	
	void OnTriggerExit2D(Collider2D col){
		if (compB.carCount > 0) {
			compB.carCount--;
		}
	}

}

