using UnityEngine;
using System.Collections;

public class EndScene : MonoBehaviour
{
	void OnMouseUpAsButton() {
		Debug.Log ("AAA");
		Application.LoadLevel("end");
	}
}

