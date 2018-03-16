using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnPatternStart : MonoBehaviour {

	void Update () {
		transform.rotation = Quaternion.Euler(0, 0, Time.time * 50);
	}
}
