using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/Enemy/EnableGravityAction")]
public class EnableGravityAction : StateAction {

    public override void ActOnce(StateController controller) {
        MelodyInteractableData data = (MelodyInteractableData)controller.data;

        controller.rb.gravityScale = data.melodyDebuffData.defaultGravityScale; //Get default gravity scale
    }
}
