using UnityEngine;
using System.Collections;

public class CarDownRight : MonoBehaviour
{
	public float RotateWeight = 1.0f;
	public Vector2 SPEED = new Vector2(0.05f, 0.05f);

    private bool isValidCarDownRight = false;
	public void cardownright()
	{
        iTween.Resume(gameObject);
	}
	
}

