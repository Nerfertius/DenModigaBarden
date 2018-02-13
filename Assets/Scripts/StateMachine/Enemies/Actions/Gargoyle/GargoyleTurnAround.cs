using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/Enemy/Gargoyle/GargoyleTurnAround")]
public class GargoyleTurnAround : StateAction {

    public override void ActOnce(StateController controller) {
        GargoyleData data = (GargoyleData)controller.data;
        data.transform.localScale = new Vector3(data.transform.localScale.x * -1, data.transform.localScale.y, data.transform.localScale.z);
        data.sight.LostSightOfPlayer();

        data.rb.velocity = new Vector2(0, data.rb.velocity.y);
        data.transform.Translate(new Vector3(-0.8f * data.transform.localScale.x, 0.5f, 0));
    }
}