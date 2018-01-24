using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Condition/General/OnKeyUp")]
public class OnKeyUp : Condition {

    public string button;


    public override bool? CheckCondition(StateController controller) {
        return Input.GetButtonUp(button);
    }

}
