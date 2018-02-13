using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/Enemy/SleepSFXStop")]
public class SleepSFXStop : StateAction {

    public override void ActOnce(StateController controller) {
        EnemyData data = (EnemyData)controller.data;
        Destroy(data.sleepSFXObject.gameObject);
        data.sleepSFXObject = null;
    }
}
