using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Condition/Player/PlayerCrouchToWalk")]
public class PlayerCrouchToWalk : Condition
{
    public override bool? CheckCondition(StateController controller)
    {
        return (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0 && controller.rb.velocity.x == 0);
    }
}
