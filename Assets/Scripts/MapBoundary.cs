using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBoundary : MonoBehaviour {
    private Camera mainCam;
    private CameraFollow2D camScript;
    private BoxCollider2D boundary;

	void Start () {
        mainCam = Camera.main;
        camScript = mainCam.GetComponent<CameraFollow2D>();
        boundary = GetComponent<BoxCollider2D>();	
	}

    void OnTriggerEnter2D (Collider2D collision)
	{
		if (collision.tag == "Player") {
            UpdateMapBounds();
		}
    }

    public void UpdateMapBounds()
    {
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float camHalfHeight = mainCam.orthographicSize;
        float camHalfWidth = screenAspect * camHalfHeight;

        Vector2 topLeft = new Vector2(boundary.bounds.min.x + camHalfWidth, boundary.bounds.max.y - camHalfHeight);
        Vector2 bottomRight = new Vector2(boundary.bounds.max.x - camHalfWidth, boundary.bounds.min.y + camHalfHeight);
        camScript.SetMapBoundary(topLeft, bottomRight);
    }
}
