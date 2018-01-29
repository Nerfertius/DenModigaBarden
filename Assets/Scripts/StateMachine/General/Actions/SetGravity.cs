using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/General/SetGravity")]
public class SetGravity : StateAction {
    public float gravityValue;

    public override void ActOnce(StateController controller)
    {
        controller.rb.gravityScale = gravityValue;
    }

}
