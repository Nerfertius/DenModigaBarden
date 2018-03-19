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
    private bool played;
    public bool playOnce;

    private Animator anim;

    private AudioClip audioClip;

    void Start()
    {
        anim = GetComponent<Animator>();
        audioClip = Resources.Load("SoundEffects/PuzzleComponents/Lever_Use") as AudioClip;
    }

    private void Update()
    {
        if (played) return;

        if (Input.GetButtonDown("Interact") && playerIsNear) {
            if (playOnce) played = true;

            if (IsActive()) {
                anim.SetBool("Active", false);
            }
            else {
                anim.SetBool("Active", true);
            }
            AudioManager.PlayOneShot(audioClip);
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
