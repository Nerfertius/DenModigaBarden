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

    [HideInInspector] public string keyButton;
    [HideInInspector] public NoteID noteID;

    [HideInInspector] public AudioClip audio;
    [HideInInspector] public int FXRowNumber;

    public Note(NoteID noteID, AudioClip audio, int FXRowNumber) {
        this.noteID = noteID;
        this.audio = audio;
        this.FXRowNumber = FXRowNumber;

        switch (noteID) {
            case Note.NoteID.G:
                keyButton = "Note G";
                break;
            case Note.NoteID.A:
                keyButton = "Note A";
                break;
            case Note.NoteID.B:
                keyButton = "Note B";
                break;
            case Note.NoteID.C:
                keyButton = "Note C";
                break;
            case Note.NoteID.D:
                keyButton = "Note D";
                break;
            case Note.NoteID.E:
                keyButton = "Note E";
                break;
            case Note.NoteID.Fplus:
                keyButton = "Note F+";
                break;
            case Note.NoteID.g8va:
                keyButton = "Note G8va";
                break;
        }
    }

    public bool CheckButton() {
        return Input.GetButtonDown(keyButton);
    }
}
