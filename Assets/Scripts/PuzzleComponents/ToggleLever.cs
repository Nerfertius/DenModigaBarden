﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleLever : MonoBehaviour {
    public List<ActivatableReceiver> recievers;

    private bool playerIsNear;

    private Animator anim;
    private BoxCollider2D collider;

    void Start() {
        anim = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
    }

    private void Update() {
        if (Input.GetButtonDown("Interact") && playerIsNear) {
            if (IsActive()) {
                anim.SetBool("Active", false);
            }
            else {
                anim.SetBool("Active", true);
            }
            foreach (ActivatableReceiver receiver in recievers) {
                receiver.Toggle();
            }
        }
    }

    public bool IsActive() {
        return anim.GetBool("Active");
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            playerIsNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            playerIsNear = false;
        }
    }
}