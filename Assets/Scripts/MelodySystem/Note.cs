using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note {

    public string Button;

    public int NoteID;

    public Note(int NoteID, string button) {
        this.Button = button;
        this.NoteID = NoteID;
    }
}
