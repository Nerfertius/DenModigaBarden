using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/Enemy/SleepSFXStart")]
public class SleepSFXStart : StateAction {

    public float angle;

    public override void ActOnce(StateController controller) {
        EnemyData data = (EnemyData)controller.data;
        
        data.sleepSFXObject = Instantiate(data.sleepSFXPrefab, data.transform.position, Quaternion.Euler(0, 0, 0), data.transform);
        if (!data.facingRight) {
            data.sleepSFXObject.transform.rotation = Quaternion.Euler(0, 0, 90 - angle);
        }
        else {
            data.sleepSFXObject.transform.rotation = Quaternion.Euler(0, 0, 90 + angle);
        }
    }
}
