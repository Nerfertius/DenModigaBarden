using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour {

    [Header("This pressure plate")]
    public List<ActivatableReceiver> recievers;

    [Tooltip("If it should go back up when leaving")]
    public bool returnOnLeave;


    [Header("Linked pressure plate")]
    [Tooltip("All linked objects must be Down/On for the recievers to 'open'")]
    public List<PressurePlate> linkedPlates;
    public bool returnOnAllDone;

    private int numberOfObjectsOnIt = 0;

    //private bool down; 
    private Animator anim; // has a bool that checks if down
    private BoxCollider2D collider;

    private bool allLinkedDown = false;

    void Start() {
        anim = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
    }

    private void Update() {
        bool lastFrameAllDown = allLinkedDown;
        allLinkedDown = AllLinkedDown();

        // Turn on 
        if (!lastFrameAllDown && allLinkedDown) {
            foreach (ActivatableReceiver receiver in recievers) {
                receiver.Activate();
            }

            if (!returnOnAllDone && linkedPlates.Count > 0) {
                returnOnLeave = false;
                foreach (PressurePlate linked in linkedPlates) {
                    linked.returnOnLeave = false;
                }
            }
        }//Turn off
        else if (lastFrameAllDown && !allLinkedDown) {
            foreach (ActivatableReceiver receiver in recievers) {
                receiver.Deactivate();
            }
        }
    }

    private bool AllLinkedDown() {
        bool allDown = IsDown();
        foreach (PressurePlate linked in linkedPlates) {
            if (!linked.IsDown()) {
                allDown = false;
                break;
            }
        }
        return allDown;
    }

    private void OnTriggerEnter2D(Collider2D coll) {
        if ((coll.gameObject.CompareTag("Player") || coll.gameObject.CompareTag("Enemy"))) {
            numberOfObjectsOnIt++;

            if(!IsDown()) {
                anim.SetBool("PressedDown", true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D coll) {
        if (returnOnLeave && (coll.gameObject.CompareTag("Player") || coll.gameObject.CompareTag("Enemy")) == true) {
            numberOfObjectsOnIt--;
            if (numberOfObjectsOnIt == 0) {
                //add delay
                anim.SetBool("PressedDown", false);
            }
        }
    }

    public bool IsDown() {
        return anim.GetBool("PressedDown");
    }
}