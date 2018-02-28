using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour {

    [Header("This pressure plate")]
    public List<ActivatableReceiver> recievers;

    [Tooltip("If it should go back up when leaving")]
    public bool returnOnLeave;
    [Tooltip("How long it takes for it to go back up when leaving")]
    public float returnDelay;


    [Header("Linked pressure plate")]
    [Tooltip("All linked objects must be Down/On for the recievers to 'open'")]
    public List<PressurePlate> linkedPlates;
    public bool returnOnAllDone;
    public float returnOnAllDoneDelay;


    //private bool down; 
    private Animator anim; // has a bool that checks if down
    private BoxCollider2D collider;

    private bool allLinkedDown = false;

    void Start() {
        anim = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
    }

    private void Update() {

        //if off check if it should be on
        if (!allLinkedDown) {
            allLinkedDown = IsDown();
            foreach (PressurePlate linked in linkedPlates) {
                if (!linked.IsDown()) {
                    allLinkedDown = false;
                    break;
                }
            }
            if (allLinkedDown){
                foreach (ActivatableReceiver receiver in recievers) {
                    receiver.Activate();
                }

                if (!returnOnAllDone) {
                    returnOnLeave = false;
                    foreach (PressurePlate linked in linkedPlates) {
                        linked.returnOnLeave = false;
                    }
                }
            }
        }//if on check if it should be off
        else if (returnOnAllDone){
            allLinkedDown = IsDown();
            foreach (PressurePlate linked in linkedPlates) {
                if (!linked.IsDown()) {
                    allLinkedDown = false;
                    break;
                }
            }
            if (!allLinkedDown) {
                foreach (ActivatableReceiver receiver in recievers) {
                    receiver.Deactivate();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D coll) {
        if ((coll.gameObject.CompareTag("Player") || coll.gameObject.CompareTag("Enemy")) && IsDown() == false) {
            anim.SetBool("PressedDown", true);
        }
    }

    private void OnTriggerExit2D(Collider2D coll) {
        if (returnOnLeave && (coll.gameObject.CompareTag("Player") || coll.gameObject.CompareTag("Enemy")) == true && IsDown() == true) {
            //add delay
            anim.SetBool("PressedDown", false);
        }
    }

    public bool IsDown() {
        return anim.GetBool("PressedDown");
    }
}