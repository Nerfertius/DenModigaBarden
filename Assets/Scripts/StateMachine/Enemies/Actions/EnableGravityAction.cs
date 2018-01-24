using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/Enemy/EnableGravityAction")]
public class EnableGravityAction : StateAction {

    public override void ActOnce(StateController controller) {
        controller.GetComponent<Rigidbody2D>().gravityScale = controller.data.melodyDebuffValues.defaultGravityScale; //Get default gravity scale
    }
}
