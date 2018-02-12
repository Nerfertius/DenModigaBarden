using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UVScrolling : MonoBehaviour {
    public bool autoScroll;
    public float autoScrollSpeed;
    public float scrollSpeed = 0.5f;
    
    private float offset;
    private MeshRenderer rend;

    void Start()
    {
        rend = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        offset = Camera.main.transform.position.x * scrollSpeed * 0.01f;

        if (autoScroll)
        {
            offset += autoScrollSpeed * Time.time;
        }

        rend.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}
