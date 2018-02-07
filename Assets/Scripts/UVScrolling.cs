using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UVScrolling : MonoBehaviour {
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
        rend.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}
