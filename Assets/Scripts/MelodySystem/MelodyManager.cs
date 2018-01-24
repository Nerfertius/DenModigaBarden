using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// turn into states?
public class NewBehaviourScript : MonoBehaviour {

    public LinkedList<Melody> melodies;

    public int MaxSavedNotes = 5;
    private LinkedList<Note> PlayedNotes;
    private Note[] Notes;

    public KeyCode ActivateKey = KeyCode.E;

    //add prefabs in inspector
    public static MelodyProjectile SleepProjectile;
    public static MelodyProjectile MagicResistProjectile;
    public static MelodyProjectile JumpProjectile;

    public void Start() {
        Notes = new Note[4];
        Notes[0] = new Note(0, KeyCode.A);
        Notes[1] = new Note(1, KeyCode.S);
        Notes[2] = new Note(2, KeyCode.D);
        Notes[3] = new Note(3, KeyCode.F);
    }

    public void Update() {
        foreach (Note note in Notes) {
            if (Input.GetKeyDown(note.KeyCode)) {
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
    }
}
