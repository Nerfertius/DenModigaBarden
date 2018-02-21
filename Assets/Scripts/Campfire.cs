using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campfire : MonoBehaviour
{
    public int order;
    public MapBoundary mb;

    private Animator anim;

    void Start ()
    {
        anim = GetComponent<Animator>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && anim.GetBool("Active") == false)
        {
            PlayerData.player.campfire = this;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerData.player.campfire = null;
        }
    }

    public void SetSpawn(PlayerData data)
    {
        if (data.currentRespawnOrder < order)
        {
            anim.SetBool("Active", true);
            if (data.respawnLocation.GetComponent<Campfire>())
            {
                data.respawnLocation.GetComponent<Animator>().SetBool("Active", false);
            }
            data.respawnLocation = transform;
            data.currentRespawnOrder = order;
        }
    }
}
