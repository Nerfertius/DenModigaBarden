using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/Enemy/Gargoyle/ColorBlinkStart")]
public class ColorBlinkStart : StateAction {

    public override void ActOnce(StateController controller) {
        GargoyleData data = (GargoyleData)controller.data;
        data.frozenColorBlinkData.Start(data.spriteRenderer);
    }
}
