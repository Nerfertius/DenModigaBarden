using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/Enemy/Gargoyle/GargoyleIdleEnter")]
public class GargoyleIdleEnter : StateAction {

    public override void ActOnce(StateController controller) {
        GargoyleData data = (GargoyleData)controller.data;

        data.dashCollider.enabled = false;
        data.idleCollider.enabled = true;
    }

}