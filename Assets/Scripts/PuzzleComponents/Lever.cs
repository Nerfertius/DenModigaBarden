using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [Header("This Lever")]
    public List<ActivatableReceiver> recievers;

    [Header("Linked Leveres")]
    [Tooltip("All linked objects must be On for the recievers to 'open'")]
    public List<Lever> linkedLevers;

    private bool playerIsNear;
    private bool allLinkedActive;

    private Animator anim;
    private BoxCollider2D collider;

    void Start()
    {
        anim = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interact") && playerIsNear) {
            if (IsActive()) {
                anim.SetBool("Active", false);
            }
            else {
                anim.SetBool("Active", true);
            }
        }


        bool lastFrameActive = allLinkedActive;
        allLinkedActive = AllLinkedActive();
        // turn on
        if (!lastFrameActive && allLinkedActive) {
            foreach (ActivatableReceiver receiver in recievers) {
                receiver.Activate();
            }
        }//Turn off
        else if(lastFrameActive && !allLinkedActive){
            foreach (ActivatableReceiver receiver in recievers) {
                receiver.Deactivate();
            }
        }
    }

    private bool AllLinkedActive() {
        bool allActive = IsActive();
        foreach (Lever l in linkedLevers) {
            if (!l.IsActive()) {
                allActive = false;
                break;
            }
        }
        return allActive;
    }

    public bool IsActive() {
        return anim.GetBool("Active");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsNear = false;
        }
    }
}
