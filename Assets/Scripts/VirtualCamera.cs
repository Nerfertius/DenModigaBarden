using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCamera : MonoBehaviour {
    private Camera cam;
    private Transform virtualScreen;

    [Range(1, 64)] public float PPU;

    void Start()
    {
        cam = GetComponent<Camera>();
        virtualScreen = transform.GetChild(0);
    }
	void Update() {
        cam.orthographicSize = Screen.height * 0.5f / PPU;

        virtualScreen.localScale = new Vector2(Screen.width / PPU, Screen.height / PPU);
    }
}
