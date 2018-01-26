using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/Enemy/DisableGravityAction")]
public class DisableGravityAction : StateAction {

    public override void ActOnce(StateController controller) {
        MelodyInteractableData data = (MelodyInteractableData)controller.data;

        controller.rb.velocity = new Vector2(controller.rb.velocity[0], 0);
        
        data.melodyDebuffData.defaultGravityScale = controller.rb.gravityScale;
        controller.rb.gravityScale = 0;
    }
}
