using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/Enemies/EnableGravityAction")]
public class EnableGravityAction : StateAction {

    public override void ActOnce(StateController controller) {
        //enable gravity
        controller.GetComponent<Rigidbody2D>().gravityScale = 1; //Get default gravity scale
    }
}
