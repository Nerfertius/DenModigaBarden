using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/Enemy/DisableGravityAction")]
public class DisableGravityAction : StateAction {

    public override void ActOnce(StateController controller) {
        controller.data.melodyDebuffValues.defaultGravityScale = controller.GetComponent<Rigidbody2D>().gravityScale;
        controller.GetComponent<Rigidbody2D>().gravityScale = 0;
    }
}
