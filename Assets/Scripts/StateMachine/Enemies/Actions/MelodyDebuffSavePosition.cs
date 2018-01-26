using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/Enemy/MelodyDebuffSavePosition")]
public class MelodyDebuffSavePosition : StateAction {

    public override void ActOnce(StateController controller) {
        MelodyInteractableData data = (MelodyInteractableData)controller.data;

        data.melodyDebuffData.debuffStartPos = (Vector2)controller.transform.position;
    }
}
