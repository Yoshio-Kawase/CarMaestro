using UnityEngine;
using System.Collections;

public class Stop : MonoBehaviour
{
    void Awake()
    {
    }

    void Update()
    {

    }
	void OnTriggerEnter2D(Collider2D col){
		col.rigidbody2D.gravityScale = 0f;
		col.rigidbody2D.velocity = Vector2.zero;
	}

}
