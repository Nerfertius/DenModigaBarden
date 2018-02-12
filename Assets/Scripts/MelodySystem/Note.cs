using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Note {

    public enum NoteID {
        Note1,
        Note2,
        Note3,
        Note4,
        Note5
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
            case Note.NoteID.Note1:
                Button = "Note1";
                break;
            case Note.NoteID.Note2:
                Button = "Note2";
                break;
            case Note.NoteID.Note3:
                Button = "Note3";
                break;
            case Note.NoteID.Note4:
                Button = "Note4";
                break;
            case Note.NoteID.Note5:
                Button = "Note5";
                break;
        }

    }
}
