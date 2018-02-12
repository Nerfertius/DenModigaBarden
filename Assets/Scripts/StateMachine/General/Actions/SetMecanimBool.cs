using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/General/SetMecanimBool")]
public class SetMecanimBool : StateAction
{
    public string parameterName;
    public bool value;

    public override void ActOnce(StateController controller)
    {
        controller.anim.SetBool(parameterName, value);
    }

}
