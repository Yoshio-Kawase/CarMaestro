using UnityEngine;
using System.Collections;

public class ChangeTop : MonoBehaviour {
	public GuageTop _pingPong;
	public TextMesh _resultText;
	Vector3 defaultScale;
	
	bool isTapped = false;
	float resultValue;
	
	public bool CHANGE_XT = false;
	public bool CHANGE_YT = false;
	public bool CHANGE_ZT = false;
	
	// Use this for initialization
	void Start () {
		_pingPong = this.gameObject.GetComponent<GuageTop>();
		defaultScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isTapped) {
			this.changeSize();
		}
		
		if (Input.GetMouseButtonDown(0)) {
			resultValue = _pingPong.value;
			_resultText.text = "RESULT: " + resultValue;
			isTapped = true;
		}
	}
	
	void changeSize() {
		Vector3 newSize = transform.localScale;
		
		if (CHANGE_XT) {
			newSize.x = defaultScale.x * _pingPong.value / _pingPong.maxValue;
		}
		if (CHANGE_YT) {
			newSize.y = defaultScale.y * _pingPong.value / _pingPong.maxValue;
		}
		
		if (CHANGE_ZT) {
			newSize.z = defaultScale.z * _pingPong.value / _pingPong.maxValue;
		}
		transform.localScale = newSize;
	}
}
