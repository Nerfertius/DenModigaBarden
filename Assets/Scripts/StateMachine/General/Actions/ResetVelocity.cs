using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/General/ResetVelocity")]
public class ResetVelocity : StateAction
{

    public override void ActOnce(StateController controller)
    {
        controller.rb.velocity = Vector2.zero;
    }

}
