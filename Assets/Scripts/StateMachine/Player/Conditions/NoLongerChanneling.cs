using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Condition/Player/NoLongerChanneling")]
public class NoLongerChanneling : Condition {

    public override bool? CheckCondition(StateController controller) {
        PlayerData data = (PlayerData)controller.data;
        PlayerData.MelodyData mData = data.melodyData;

        return mData.currentMelody == null;
    }
}
