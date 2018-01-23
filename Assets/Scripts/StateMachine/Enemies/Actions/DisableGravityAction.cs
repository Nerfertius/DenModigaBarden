using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/Enemies/DisableGravityAction")]
public class DisableGravityAction : StateAction {

    public override void ActOnce(StateController controller) {
        //disable gravity
        controller.GetComponent<Rigidbody2D>().gravityScale = 0;
    }
}
