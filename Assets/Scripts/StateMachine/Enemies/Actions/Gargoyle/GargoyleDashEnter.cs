using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/Enemy/Gargoyle/GargoyleDashEnter")]
public class GargoyleDashEnter : StateAction {

    public override void ActOnce(StateController controller) {
        GargoyleData data = (GargoyleData)controller.data;

        data.dashCollider.enabled = true;
        data.idleCollider.enabled = false;
        data.transitFromFrozen = false;
    }
}