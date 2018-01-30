using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "StateMachine/Action/General/SetMecanimTrigger")]
public class SetMecanimTrigger : StateAction {
    public string parameterName;

    public override void ActOnce(StateController controller) {
        controller.anim.SetTrigger(parameterName);
    }

}
