using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBoundary : MonoBehaviour {
    private Camera mainCam;
    private CameraFollow2D camScript;
    private BoxCollider2D boundary;
    private static Vector2 newTopLeft;
    private static Vector2 newBottomRight;
    private static bool firstTime;
    
	void Start () {
        mainCam = Camera.main;
        camScript = mainCam.GetComponent<CameraFollow2D>();
        boundary = GetComponent<BoxCollider2D>();

        firstTime = true;
	}
    
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            CalculateMapBounds();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && firstTime)
        {
            CalculateMapBounds();
            UpdateMapBounds();
            firstTime = false;
        } else if (collision.tag == "Player")
        {
            CalculateMapBounds();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            UpdateMapBounds();
            camScript.ActivateTransition();
            GameManager.instance.switchState(new TransitionState(GameManager.instance));
        }
    }

    public void CalculateMapBounds()
    {
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float camHalfHeight = mainCam.orthographicSize;
        float camHalfWidth = screenAspect * camHalfHeight;

        newTopLeft = new Vector2(boundary.bounds.min.x + camHalfWidth, boundary.bounds.max.y - camHalfHeight);
        newBottomRight = new Vector2(boundary.bounds.max.x - camHalfWidth, boundary.bounds.min.y + camHalfHeight);
    }

    public void UpdateMapBounds()
    {
        camScript.SetMapBoundary(newTopLeft, newBottomRight);
    }
}
