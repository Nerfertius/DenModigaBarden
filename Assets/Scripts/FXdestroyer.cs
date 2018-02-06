using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXdestroyer : MonoBehaviour
{
    [HideInInspector]public bool hasPlayed;
    private ParticleSystem ps;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }
    void Update()
    {
        if (hasPlayed && !ps.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
