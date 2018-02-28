using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hatch : MonoBehaviour
{
    private bool opened;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Input.GetButtonDown("Interact") && !opened)
        {
            Open();
        }
    }

    void Open()
    {
        GetComponent<Animator>().SetTrigger("Open");
        opened = true;
    }
}
