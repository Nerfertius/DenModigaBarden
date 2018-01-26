using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Condition/Player/MelodyPlayed")]
public class MelodyPlayed : Condition {

    public override bool? CheckCondition(StateController controller) {
        
        if (Input.GetButtonUp("PlayMelody")) {
            Debug.Log("hey");
            PlayerData data = (PlayerData)controller.data;
            PlayerData.MelodyManagerData mData = data.melodyManagerData;

            foreach (Melody melody in mData.melodies) {
                if (melody.CheckMelody(mData.PlayedNotes)) {
                    mData.currentMelody = melody.melodyID;
                    mData.PlayedNotes.Clear();
                    return true;
                }
            }
            mData.PlayedNotes.Clear();
            return false;
        }
        return null;
        
    }
}
