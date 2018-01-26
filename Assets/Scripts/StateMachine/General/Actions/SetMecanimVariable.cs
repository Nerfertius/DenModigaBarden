using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/General/SetMecanimVariable")]
public class SetMecanimVariable : StateAction
{
    public override void FixedAct(StateController controller)
    {
        controller.rb.velocity = Vector2.zero;
        controller.sprRend.flipX = !controller.sprRend.flipX;
    }

}
