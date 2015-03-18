using UnityEngine;
using System.Collections;

public class CarDownLeft : MonoBehaviour
{
	public float RotateWeight = 1.0f;
	public Vector2 SPEED = new Vector2(0.05f, 0.05f);
	
	private bool isValidCarDownLeft = false;
			
	public void Update()
	{
	    if (true == isValidCarDownLeft) {
    		Vector2 Position = transform.position;
    		float pos = 2.5f;
    		if (Position.y > pos) {
    			GameObject target = GameObject.Find ("spawner");
    			Spawner sp = target.GetComponent<Spawner> ();
    			sp.carCount--;
    			Position.y -= SPEED.y;
    			// 現在の位置に加算減算を行ったPositionを代入する
    			transform.position = Position;
    		} else {
    			if (-90.0 <= GetComponent<Rigidbody2D>().rotation) {
    				GetComponent<Rigidbody2D>().MoveRotation (GetComponent<Rigidbody2D>().rotation - 1.5f);
    				Vector2 rotatePos = new Vector2 (
    					Mathf.Cos ((GetComponent<Rigidbody2D>().rotation + 270.0f) * Mathf.Deg2Rad),
    					Mathf.Sin ((GetComponent<Rigidbody2D>().rotation + 270.0f) * Mathf.Deg2Rad));
    				GetComponent<Rigidbody2D>().velocity = new Vector2 (
    					GetComponent<Rigidbody2D>().velocity.x * rotatePos.x + GetComponent<Rigidbody2D>().velocity.x * rotatePos.x,
    					GetComponent<Rigidbody2D>().velocity.y * rotatePos.y + GetComponent<Rigidbody2D>().velocity.y * rotatePos.x);
    				GetComponent<Rigidbody2D>().AddForce (rotatePos / RotateWeight);
    				GetComponent<Rigidbody2D>().velocity = Vector2.ClampMagnitude (GetComponent<Rigidbody2D>().velocity, 1.0f);
    			}
    		}
    	}
	}
	
	public void cardownleft()
	{
        isValidCarDownLeft = true;
	}

}

