using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : Data
{
	[Header("Movement Settings")]
	[Range(0, 10)] public float maxSpeed;
	[Range(0, 100)] public float speedMod;
	[Range(100, 500)] public float jumpPower;
	[Range(0, 10)] public float climbSpeed;

	[Space(10)]
	public LayerMask groundLayer;


	[HideInInspector] public bool climbing;
	[HideInInspector] public bool grounded;
	[HideInInspector] public float moveHorizontal;
	[HideInInspector] public float moveVertical;
	[HideInInspector] public Vector2 movement;
	[HideInInspector] public Rigidbody2D body;
	[HideInInspector] public Transform groundCheck;

    [HideInInspector] public GameObject ladderBottom;
	[HideInInspector] public GameObject ladderTop;

    // Variables used by Camera
    [HideInInspector] public bool inTransit;
    [HideInInspector] public Vector2 targetPos;

	void Start ()
	{
		groundCheck = transform.GetChild(0);
		body = GetComponent<Rigidbody2D>();
	}
}
