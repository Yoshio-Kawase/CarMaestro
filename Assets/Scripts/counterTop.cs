using UnityEngine;
using System.Collections;

public class counterTop : MonoBehaviour
{

	public GameObject tarT;
	public Spawner compT;
	public int clearTop = 0;
	
	void Start(){
		tarT = GameObject.Find ("spawner");
		compT = tarT.GetComponent<Spawner> ();
	}
	
	void OnTriggerExit2D(Collider2D col){
		if (compT.carCount > 0) {
			compT.carCount--;
			clearTop++;
		}
	}

}

