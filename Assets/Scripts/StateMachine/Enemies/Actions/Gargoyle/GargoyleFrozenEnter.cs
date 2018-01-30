using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/Enemy/Gargoyle/GargoyleFrozenEnter")]
public class GargoyleFrozenEnter : StateAction {

    public override void ActOnce(StateController controller) {
        GargoyleData data = (GargoyleData)controller.data;

        data.rb.constraints = RigidbodyConstraints2D.FreezeAll;
        data.gameObject.layer = 8; // Blockable
        data.harmful = false;
        data.transitFromFrozen = true;
    }

}