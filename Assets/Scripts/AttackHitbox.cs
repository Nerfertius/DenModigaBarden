using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitbox : MonoBehaviour {
	public float delayTime;
	public float expansionTime;
	public Vector2 collNewSize;

	private float timer;
	private BoxCollider2D coll;

	private void Start(){
		coll = GetComponent<BoxCollider2D>();
	}

	private void Update ()
	{
		timer += Time.deltaTime;

		if (timer >= delayTime) {
			coll.enabled = true;

			if (timer >= expansionTime) {
				coll.size = collNewSize;
			}
		}
	}
}
