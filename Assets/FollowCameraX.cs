using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCameraX : MonoBehaviour {

    void Update()
    {
        transform.position = new Vector3(Camera.main.transform.position.x, transform.position.y, transform.position.z);
    }
}
