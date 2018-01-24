using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melody {

    public enum MelodyID {
        JumpMelody,
        SleepMelody,
        MagicResistMelody
    }

    public MelodyID melodyID;
    public Note[] Notes;


    public bool CheckMelody(LinkedList<Note> notesPlayed) {

        int shortest = Mathf.Min(Notes.Length, notesPlayed.Count);
        int longest = Mathf.Max(Notes.Length, notesPlayed.Count);

        int sizeDiff = longest - shortest;

        LinkedListNode<Note> it = notesPlayed.Last;

        for(int i = Notes.Length; i >= 0; i--) {
            if (Notes[i] != it.Value) {
                return false;
            }
            if(it.Previous != null) {
                it = it.Previous;
            }
        }
        return true;

    }
}
