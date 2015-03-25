using UnityEngine;
using System.Collections;

public class ChangeBottom : MonoBehaviour {
	public GuageBottom _pingPong;
	Vector3 defaultScale;
	
	// Use this for initialization
	void Start () {
		_pingPong = this.gameObject.GetComponent<GuageBottom>();
		defaultScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		this.changeSize();
	}
	
	void changeSize() {
		Vector3 newSize = transform.localScale;
		newSize.x = defaultScale.x * _pingPong.value / _pingPong.maxValue;

		transform.localScale = newSize;
	}
}
