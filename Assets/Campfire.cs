using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campfire : MonoBehaviour
{
    public int order;

    private Animator anim;

	void Start ()
    {
        anim = GetComponent<Animator>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && anim.GetBool("Active") == false)
        {
            if (collision.GetComponent<PlayerData>().currentRespawnOrder < order)
            {
                anim.SetBool("Active", true);
                if (collision.GetComponent<PlayerData>().respawnLocation.GetComponent<Campfire>())
                {
                    collision.GetComponent<PlayerData>().respawnLocation.GetComponent<Animator>().SetBool("Active", false);
                }
                collision.GetComponent<PlayerData>().respawnLocation = transform;
                collision.GetComponent<PlayerData>().currentRespawnOrder = order;
            }
        }
    }
}
