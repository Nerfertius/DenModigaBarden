using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/Enemy/Gargoyle/GargoyleDashAction")]
public class GargoyleDashAction : StateAction {

	public override void FixedAct(StateController controller) {
        GargoyleData data = (GargoyleData)controller.data;
        float direction = data.transform.localScale.x > 0 ? 1 : -1;

        data.transform.Translate(data.speed * Time.fixedDeltaTime * direction, 0, 0);
    }

}
