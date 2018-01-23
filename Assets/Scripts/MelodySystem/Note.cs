using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note {

    public KeyCode KeyCode;

    public int NoteID;

    public Note(int NoteID, KeyCode keycode) {
        this.KeyCode = keycode;
        this.NoteID = NoteID;
    }
}
