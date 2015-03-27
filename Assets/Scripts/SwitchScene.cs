using UnityEngine;
using System.Collections;

public class SwitchScene : MonoBehaviour
{
	private IEnumerator OnMouseUpAsButton() {
		yield return new WaitForSeconds(1);
		Application.LoadLevel ("start2");
	}
}

