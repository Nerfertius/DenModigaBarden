using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightColliderAV : MonoBehaviour {
    [HideInInspector] public PlayerData player;

    public void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag == "Player") {
            player = coll.GetComponent<PlayerData>();
        }  
    }

    public void OnTriggerExit2D(Collider2D coll)
    {
        if (player != null && coll.tag == "Player")
        {
            player = null;
        }
    }

    public bool PlayerInRange() {
        return player != null;
    }

    public void LostSightOfPlayer() {
        player = null;
    }
}
