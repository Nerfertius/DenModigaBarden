using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraFollow2D : MonoBehaviour {

	public Transform target;
    public Transform[] backgrounds;
    public bool hideCursor;
    [Range(1, 10)] public float followSpeed;
    [Range(1, 10)] public float transitionSpeed;

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
			transform.position = Vector3.MoveTowards (transform.position, new Vector3 (posX, posY, transform.position.z), 0.5f * followSpeed);

            UpdateBackgroundPosition();
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
			transform.position = Vector3.MoveTowards(transform.position, new Vector3 (posX, posY, transform.position.z), 15 * transitionSpeed * Time.unscaledDeltaTime);
            UpdateBackgroundPosition();
        } else {
            GameManager.instance.switchState(new PlayState(GameManager.instance));
            transitioning = false;
		}
	}

    public void SetMapBoundary(Vector2 topLeft, Vector2 bottomRight)
    {
        this.topLeft = topLeft;
        this.bottomRight = bottomRight;
    }

    public void ActivateTransition()
    {
        transitioning = true;
    }

    public void UpdateToMapBounds()
    {
        posX = Mathf.Clamp(target.transform.position.x, topLeft.x, bottomRight.x);
        posY = Mathf.Clamp(target.transform.position.y, bottomRight.y, topLeft.y);

        transform.position = new Vector3(posX, posY, transform.position.z);
        UpdateBackgroundPosition();
    }

    private void UpdateBackgroundPosition()
    {
        foreach (Transform bg in backgrounds)
        {
            bg.transform.position = new Vector3(transform.position.x, bg.transform.position.y, bg.transform.position.z);
        }
    }
}
