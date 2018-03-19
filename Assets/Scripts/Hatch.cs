using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hatch : MonoBehaviour
{
    private bool opened;
    private bool playerNear;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerNear = false;
        }
    }

    private void Update()
    {
        if (playerNear && Input.GetButtonDown("Interact") && !opened && !PlayerData.player.melodyData.playMelodyState)
        {
            GetComponent<Animator>().SetBool("Open", true);

            GetComponent<EdgeCollider2D>().enabled = false;

            opened = true;
        }
    }
}
