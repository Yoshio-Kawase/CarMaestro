using UnityEngine;
using System.Collections;

public class clear : MonoBehaviour
{
	public GameObject objL;
	public GameObject objR;
	public GameObject objT;
	public GameObject objB;
	public counterLeft getL;
	public counterRight getR;
	public counterTop getT;
	public counterBottom getB;
	private int clearCount;
	
	void Start(){
		objL = GameObject.Find ("counterLeft");
		objR = GameObject.Find ("counterRight");
		objT = GameObject.Find ("counterTop");
		objB = GameObject.Find ("counterBottom");
		getL = objL.GetComponent<counterLeft> ();
		getR = objR.GetComponent<counterRight> ();
		getT = objT.GetComponent<counterTop> ();
		getB = objB.GetComponent<counterBottom> ();
	}
	
	void Update(){
		clearCount = getL.clearLeft + getR.clearRight + getB.clearBottom + getT.clearTop;
		if (clearCount >= 20) {
			Application.LoadLevel ("clear");
		}
	}

}

