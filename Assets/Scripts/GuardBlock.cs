﻿using System.Collections;
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && controller.currentState.ToString() == "NPCSleep (State)")
        {
            anim.SetBool("Open", true);
            Destroy(GetComponent<Collider>());
        }
    }
}
