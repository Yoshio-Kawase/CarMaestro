using UnityEngine;
using System.Collections;

public class counterRight : MonoBehaviour
{

	public GameObject tarR;
	public Spawner compR;
	public int clearRight = 0;
	
	void Start(){
		tarR = GameObject.Find ("spawner_right");
		compR = tarR.GetComponent<Spawner> ();
	}
	
	void OnTriggerExit2D(Collider2D col){
		if (compR.carCount > 0) {
			compR.carCount--;
			clearRight++;
		}
	}

}

