using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightCollider : MonoBehaviour {
    private EnemyData eData;

    private void Start()
    {
        eData = GetComponentInParent<EnemyData>();
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            eData.player = coll.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            eData.player = null;
        }
    }
}
