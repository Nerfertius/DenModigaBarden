using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{
	[Range(0, 10)] public float maxSpeed;
	[Range(0,100)] public float speedMod;
	[Range(100, 500)] public float jumpPower;
	[Range(0, 10)] public float climbSpeed;
	public Transform groundCheck;
	public LayerMask groundLayer;

	private bool climbing;
	private bool grounded;
	private float moveHorizontal;
	private float moveVertical;
	private Vector2 movement;
	private Rigidbody2D body;

	private GameObject ladderBottom;
	private GameObject ladderTop;

	void Start ()
	{
		body = GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate ()
	{
		if (climbing == false)
		{
			//Walk state
			moveHorizontal = Input.GetAxis("Horizontal");
			movement = new Vector2(moveHorizontal, 0);
			body.AddForce(movement * speedMod);

			if (Mathf.Abs(body.velocity.x) > maxSpeed)
			{
				if (body.velocity.x > 0)
				{
					body.velocity = new Vector2(maxSpeed, body.velocity.y);
				}
				else if (body.velocity.x < 0)
				{
					body.velocity = new Vector2(-maxSpeed, body.velocity.y);
				}
			}
		}
		else if (climbing == true)
		{
			//Climb state
			moveVertical = Input.GetAxis("Vertical");
			transform.Translate(new Vector2(0, moveVertical) * climbSpeed * Time.deltaTime);
			if (transform.position.y < ladderBottom.transform.position.y - 0.1f)
			{
				transform.position = new Vector2(transform.position.x, ladderBottom.transform.position.y);
				body.isKinematic = false;
				climbing = false;
			}
			else if (transform.position.y > ladderTop.transform.position.y)
			{
				transform.position = new Vector2(transform.position.x, ladderTop.transform.position.y + 0.7f);
				body.isKinematic = false;
				climbing = false;
			}
		}
	}

	private void Update()
	{
		//AirState
		grounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

		//WalkState
		if (Input.GetKeyDown(KeyCode.Space) && grounded == true && climbing == false)
		{
			body.AddForce(new Vector2(0, jumpPower));
			grounded = false;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		//WalkState
		//AirState
		if (collision.CompareTag("Ladder") && climbing == false)
		{
			ladderBottom = collision.transform.parent.GetChild(0).gameObject;
			ladderTop = collision.transform.parent.GetChild(1).gameObject;
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		//WalkState
		//AirState
		if (collision.CompareTag("Ladder") && climbing == false)
		{
			if (Input.GetKey(KeyCode.W) && transform.position.y < ladderBottom.transform.position.y)	//Bottom
			{
				body.isKinematic = true;
				body.velocity = Vector2.zero;
				climbing = true;
				transform.position = new Vector2(collision.transform.position.x, collision.transform.position.y);
			}
			else if (Input.GetKey(KeyCode.S) && transform.position.y > ladderTop.transform.position.y)	//Top
			{
				body.isKinematic = true;
				body.velocity = Vector2.zero;
				climbing = true;
				transform.position = new Vector2(collision.transform.position.x, collision.transform.position.y + 0.5f);
			}
																										//Between
			else if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W)) && transform.position.y < ladderTop.transform.position.y && transform.position.y > ladderBottom.transform.position.y)
			{
				body.isKinematic = true;
				body.velocity = Vector2.zero;
				climbing = true;
				transform.position = new Vector2(collision.transform.position.x, transform.position.y);
			}
		}
	}
}