using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Melody {

    public enum MelodyID {
        JumpMelody,
        SleepMelody,
        MagicResistMelody
    }

    public MelodyID melodyID;
    public Note[] Notes;

    public Melody(MelodyID melodyID, Note[] notes) {
        this.melodyID = melodyID;
        this.Notes = notes;
    }


    public bool CheckMelody(LinkedList<Note> notesPlayed) {
        LinkedListNode<Note> it = notesPlayed.Last;
        if(it != null) {
            for (int i = Notes.Length - 1; i >= 0; i--) {
                if (it == null || Notes[i].noteID != it.Value.noteID) {
                    return false;
                }
                it = it.Previous;
            }
            return true;
        }
        return false;
    }
}
