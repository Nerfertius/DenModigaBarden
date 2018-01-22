using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightCollider : MonoBehaviour {
    private Collider2D coll;
    private EnemyData eData;

    private void Start()
    {
        coll = GetComponent<Collider2D>();
        eData = GetComponentInParent<EnemyData>();
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.tag == "Player")
        {
            eData.playerFound = true;
        }

        Debug.Log(eData.playerFound);
    }
}
