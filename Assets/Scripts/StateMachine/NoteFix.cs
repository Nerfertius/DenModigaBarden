using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteFix : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetButtonDown("Interact") && !PlayerData.player.hasReadNote)
        {
            PlayerData.player.hasReadNote = true;
        }
    }
}