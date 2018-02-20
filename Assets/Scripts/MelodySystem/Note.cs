using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Note {

    public enum NoteID {
        G,
        A,
        B,
        C,
        D,
        E,
        Fplus,
        g8va
    }

    [HideInInspector] public string Button;
    [HideInInspector] public NoteID noteID;

    [HideInInspector] public AudioClip audio;
    [HideInInspector] public int FXRowNumber;

    public Note(NoteID noteID, AudioClip audio, int FXRowNumber) {
        this.noteID = noteID;
        this.audio = audio;
        this.FXRowNumber = FXRowNumber;

        switch (noteID) {
            case Note.NoteID.G:
                Button = "Note G";
                break;
            case Note.NoteID.A:
                Button = "Note A";
                break;
            case Note.NoteID.B:
                Button = "Note B";
                break;
            case Note.NoteID.C:
                Button = "Note C";
                break;
            case Note.NoteID.D:
                Button = "Note D";
                break;
            case Note.NoteID.E:
                Button = "Note E";
                break;
            case Note.NoteID.Fplus:
                Button = "Note F+";
                break;
            case Note.NoteID.g8va:
                Button = "Note G8va";
                break;
        }
    }
}
