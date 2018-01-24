using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// turn into states?
public class MelodyManager : MonoBehaviour {

    public void Update() {

    }

   /* public void Update() {
        foreach (Note note in Notes) {
            if (Input.GetButtonDown(note.Button)) {
                PlayedNotes.AddLast(note);
            }
        }

        while (PlayedNotes.Count > MaxSavedNotes) {
            PlayedNotes.RemoveFirst();
        }

        //CheckForMelody();
    }

    public void CheckForMelody() {
        foreach (Melody melody in melodies) {
            if (melody.CheckMelody(PlayedNotes)) {

            }
        }
    }*/
}
