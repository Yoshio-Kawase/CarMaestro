using UnityEngine;
using System.Collections;

public class SwitchScene : MonoBehaviour
{
	void OnMouseUpAsButton() {
		Application.LoadLevel ("main");
	}

	public Rect zahyou1 = new Rect (     0,   0, 100, 50);
	public Rect zahyou2 = new Rect (   50,   0, 100, 50);
	public Rect zahyou3 = new Rect ( 100,   0, 100, 50);
	
	void OnGUI()
	{
		GUI.Label ( zahyou1, "SCORE");
		GUI.Label ( zahyou2, "Time");
		GUI.Label ( zahyou3, "Cube");
	}
}

