using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardBlock : MonoBehaviour
{
    public GameObject guardCaptain;
    private StateController controller;
    private Animator anim;

    private void Start()
    {
        controller = guardCaptain.GetComponent<StateController>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Vector2.Distance(PlayerData.player.transform.position, controller.transform.position) <= 15f && controller.currentState.ToString() == "NPCSleep (State)"
            && !anim.GetBool("Open"))
        {
            anim.SetBool("Open", true);
            foreach (Collider2D col in GetComponents<Collider2D>())
            {
                col.enabled = false;
            }
        }
    }
}
