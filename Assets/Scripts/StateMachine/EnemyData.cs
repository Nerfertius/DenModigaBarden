using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : Data {
    [HideInInspector] public bool playerFound = false;

    public float speed;
    public float weight;
    

    private Collider2D sightColl;

    private void Start()
    {
        if(transform.childCount != 0) { 
            sightColl = GetComponentsInChildren<Collider2D>()[1];
        }
    }
}
