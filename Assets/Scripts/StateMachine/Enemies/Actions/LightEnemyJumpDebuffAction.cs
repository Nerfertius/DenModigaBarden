using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/Enemy/LightEnemyJumpDebuffAction")]
public class LightEnemyJumpDebuffAction : StateAction {

    //public float Duration = 5f;  state handles this
    public float FloatHeight = 3f;
    public float FloatSpeed = 1f;


    public override void FixedAct(StateController controller) {
        float StartingHeight = controller.data.melodyDebuffValues.debuffStartPos.y;

        if (controller.transform.position.y < StartingHeight + FloatHeight) {
            controller.transform.Translate(0, FloatSpeed * Time.deltaTime, 0);
        }
        if(controller.transform.position.y > StartingHeight + FloatHeight) {
            controller.transform.position.Set(controller.transform.position.x,  (float)StartingHeight + FloatHeight, controller.transform.position.z);
        }
    }

}
