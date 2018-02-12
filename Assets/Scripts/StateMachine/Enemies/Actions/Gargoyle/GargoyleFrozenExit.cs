using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/Enemy/Gargoyle/GargoyleFrozenExit")]
public class GargoyleFrozenExit : StateAction {

    public override void ActOnce(StateController controller) {
        GargoyleData data = (GargoyleData)controller.data;

        data.rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        data.gameObject.layer = 11; // Enemy
        data.harmful = true;

        data.rb.velocity = data.velocityBeforeFrozen;
    }
}
