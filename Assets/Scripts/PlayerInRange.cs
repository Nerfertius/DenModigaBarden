using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInRange : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            ((NPCData)transform.parent.GetComponent<StateController>().data).playerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ((NPCData)transform.parent.GetComponent<StateController>().data).playerInRange = false;
        }
    }
}