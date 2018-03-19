using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleLever : MonoBehaviour {
    public List<ActivatableReceiver> recievers;

    private bool playerIsNear;

    private Animator anim;

    private AudioClip audioClip;

    void Start() {
        anim = GetComponent<Animator>();
        audioClip = Resources.Load("SoundEffects/PuzzleComponents/Lever_Use") as AudioClip;
    }

    private void Update() {
        if (Input.GetButtonDown("Interact") && playerIsNear && !PlayerData.player.melodyData.playMelodyState) {
            if (IsActive()) {
                anim.SetBool("Active", false);
            }
            else {
                anim.SetBool("Active", true);
            }
            foreach (ActivatableReceiver receiver in recievers) {
                receiver.Toggle();
            }
            AudioManager.PlayOneShot(audioClip);
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
