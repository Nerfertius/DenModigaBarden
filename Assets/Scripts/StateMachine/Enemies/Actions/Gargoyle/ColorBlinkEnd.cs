using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/Enemy/Gargoyle/ColorBlinkEnd")]
public class ColorBlinkEnd : StateAction {

    public override void ActOnce(StateController controller) {
        GargoyleData data = (GargoyleData)controller.data;
        data.frozenColorBlinkData.End(data.spriteRenderer);
    }
}
