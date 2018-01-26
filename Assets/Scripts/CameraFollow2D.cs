using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraFollow2D : MonoBehaviour {

	public Transform target;
    public bool hideCursor;
    [Range(1, 10)] public int followSpeed;
    [Range(1, 10)] public int transitionSpeed;

    Vector2 topLeft, bottomRight;
    bool transitioning = false;
    float posX, posY;

    void Start(){
    	Cursor.visible = !hideCursor;
    }

	void LateUpdate ()
	{
		posX = Mathf.Clamp (target.transform.position.x, topLeft.x, bottomRight.x);
		posY = Mathf.Clamp (target.transform.position.y, bottomRight.y, topLeft.y);

		if (!transitioning) {
			transform.position = Vector3.MoveTowards (transform.position, new Vector3 (posX, posY, transform.position.z), 0.64f * followSpeed);
			//transform.position = new Vector3 (posX, posY, transform.position.z);
		} else {
			RoomTransition ();
		}

		if (Input.GetKeyDown (KeyCode.LeftAlt)) {
			Cursor.visible = !Cursor.visible;
		}
    }

	void RoomTransition ()
	{
		if (transform.position.x < topLeft.x || transform.position.x > bottomRight.x || transform.position.y > topLeft.y || transform.position.y < bottomRight.y) {
			transform.position = Vector3.MoveTowards(transform.position, new Vector3 (posX, posY, transform.position.z), 15 * transitionSpeed * Time.deltaTime);
		} else {
			transitioning = false;
		}
	}

    public void SetMapBoundary(Vector2 topLeft, Vector2 bottomRight)
    {
        this.topLeft = topLeft;
        this.bottomRight = bottomRight;
        transitioning = true;
    }
}
