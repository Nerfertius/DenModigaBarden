using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Condition/General/OnKeyDown")]
public class OnKeyDown : Condition {

    public string button;


    public override bool? CheckCondition(StateController controller) {
        return Input.GetButtonDown(button);
    }

}
