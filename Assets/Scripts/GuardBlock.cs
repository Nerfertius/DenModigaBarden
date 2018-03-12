using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardBlock : MonoBehaviour
{
    public GameObject guardCaptain;
    private StateController controller;

    private void Start()
    {
        controller = guardCaptain.GetComponent<StateController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && controller.currentState.ToString() == "NPCSleep (State)")
        {
            Destroy(gameObject);
        }
    }
}
