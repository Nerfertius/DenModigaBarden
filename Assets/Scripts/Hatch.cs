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
        if (playerNear && Input.GetButtonDown("Interact") && !opened)
        {
            GetComponent<Animator>().SetBool("Open", true);
            opened = true;
        }
    }
}
