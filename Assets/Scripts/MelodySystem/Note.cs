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
    [HideInInspector] public string DPadButton;
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
                DPadButton = "Note G Dpad";
                break;
            case Note.NoteID.A:
                keyButton = "Note A";
                DPadButton = "Note A Dpad";
                break;
            case Note.NoteID.B:
                keyButton = "Note B";
                DPadButton = "Note B Dpad";
                break;
            case Note.NoteID.C:
                keyButton = "Note C";
                DPadButton = "Note C Dpad";
                break;
            case Note.NoteID.D:
                keyButton = "Note D";
                DPadButton = "Note D Dpad";
                break;
            case Note.NoteID.E:
                keyButton = "Note E";
                DPadButton = "Note E Dpad";
                break;
            case Note.NoteID.Fplus:
                keyButton = "Note F+";
                DPadButton = "Note F+ Dpad";
                break;
            case Note.NoteID.g8va:
                keyButton = "Note G8va";
                DPadButton = "Note G8va Dpad";
                break;
        }
    }

    public bool CheckButton() {
        return Input.GetButtonDown(keyButton) || InputExtender.GetAxisDown(DPadButton);
    }
}
