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
        if ("stop" == col.name) {
            iTween.Pause(gameObject);
        }
	}

}
