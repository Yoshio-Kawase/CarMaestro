using UnityEngine;
using System.Collections;

public class CarDown : MonoBehaviour
{
	
	public Vector2 SPEED = new Vector2(0.05f, 0.05f);

    private bool isValidCarDown = false;

    public void Update() {
        if (true == isValidCarDown) {
            Vector2 Position = transform.position;
            GameObject target = GameObject.Find("spawner");
            Spawner sp = target.GetComponent<Spawner>();
            sp.carCount--;
            Position.y -= SPEED.y;
            // 現在の位置に加算減算を行ったPositionを代入する
            transform.position = Position;
        }
    }

	public void cardown()
	{
        isValidCarDown = true;
	}
}

