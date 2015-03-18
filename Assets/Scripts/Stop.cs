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
//		col.GetComponent<Rigidbody2D>().gravityScale = 0f;
//		col.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        if ("stop" == col.name) {
            iTween.Pause(gameObject);
        }
	}

}
