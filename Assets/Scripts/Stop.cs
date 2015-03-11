using UnityEngine;
using System.Collections;

public class Stop : MonoBehaviour
{
    public float RotateWeight = 1.0f;
    private bool isStop = false;
	private Vector2 start_posi;
	private Vector2 move_posi;
	public Vector2 SPEED = new Vector2(0.05f, 0.05f);
	private int flag = 0;

    void Awake()
    {
        isStop = false;
		flag = 0;
    }

    void Update()
    {

		// 現在位置をPositionに代入
		Vector2 Position = transform.position;
		// 画像サイズの取得
		float pos = 2.5f;
		if (false == isStop) {
			return;
		}
		if (flag == 0) {
			GameObject target = GameObject.Find ("spawner");
			Spawner sp = target.GetComponent<Spawner> ();	
			sp.carCount--;
			flag++;
		}
#if false
		if (Position.y > pos) {
			Position.y -= SPEED.y;
			// 現在の位置に加算減算を行ったPositionを代入する
			transform.position = Position;
		} else {
			if (90.0 >= rigidbody2D.rotation) {
				rigidbody2D.MoveRotation (rigidbody2D.rotation + 2.5f);
				Vector2 rotatePos = new Vector2 (
					Mathf.Sin ((rigidbody2D.rotation - 90.0f) * Mathf.Deg2Rad),
					Mathf.Cos ((rigidbody2D.rotation - 90.0f) * Mathf.Deg2Rad));
				rigidbody2D.velocity = new Vector2 (
	                rigidbody2D.velocity.x * rotatePos.x + rigidbody2D.velocity.x * rotatePos.y,
	                rigidbody2D.velocity.y * rotatePos.x + rigidbody2D.velocity.y * rotatePos.y);
				rigidbody2D.AddForce (rotatePos / RotateWeight);
				rigidbody2D.velocity = Vector2.ClampMagnitude (-rigidbody2D.velocity, 1.0f);
			}
		}
#endif

    }
	void OnTriggerEnter2D(Collider2D col){
		col.rigidbody2D.gravityScale = 0f;
		col.rigidbody2D.velocity = Vector2.zero;
        isStop = true;
	}

}
