﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/Player/PlayerPlayMelody")]
public class PlayerPlayMelody : StateAction {


    public override void Act(StateController controller) {
        PlayerData data = (PlayerData)controller.data;
        PlayerData.MelodyManagerData mData = data.melodyManagerData;

        foreach (Note note in mData.Notes) {
            if (Input.GetButtonDown(note.Button)) {
                mData.PlayedNotes.AddLast(note);
            }
        }

        while (mData.PlayedNotes.Count > mData.MaxSavedNotes) {
            mData.PlayedNotes.RemoveFirst();
        }
    }
}
