using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteFix : MonoBehaviour
{
    public bool note, orcQuest;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetButtonDown("Interact") && note && !PlayerData.player.hasReadNote && !PlayerData.player.melodyData.playMelodyState)
        {
            PlayerData.player.hasReadNote = true;
        }
        else if (orcQuest && !PlayerData.player.orcQuestDone && transform.parent.GetComponent<StateController>().currentState.ToString() == "NPCSleep (State)")
        {
            PlayerData.player.orcQuestDone = true;
        }
    }
}