using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Condition/Player/MelodyPlayed")]
public class MelodyPlayed : Condition {

    public override bool? CheckCondition(StateController controller) {
        
        if (Input.GetButtonUp("PlayMelody")) {
            PlayerData data = (PlayerData)controller.data;
            PlayerData.MelodyData mData = data.melodyData;

            foreach (Melody melody in mData.melodies) {
                if (melody.CheckMelody(mData.PlayedNotes)) {

                    mData.currentMelody = melody.melodyID;
                    mData.PlayedNotes.Clear();
                    mData.MelodyRange.enabled = true;
                    return true;
                }
            }
            mData.PlayedNotes.Clear();
            return false;
        }
        return null;
        
    }
}
