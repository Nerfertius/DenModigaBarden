using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/Enemy/Gargoyle/GargoyleIdleEnter")]
public class GargoyleIdleEnter : StateAction {

    public override void ActOnce(StateController controller) {
        GargoyleData data = (GargoyleData)controller.data;

        data.switchingCollider = true;

        data.dashCollider.enabled = false;
        data.idleCollider.enabled = true;

        data.rb.velocity = new Vector2(0, data.rb.velocity.y);
        data.transform.Translate(new Vector3(-0.8f * data.transform.localScale.x, 0.5f, 0));

        data.ToggleColliderSwitchCoroutine();
    }

}