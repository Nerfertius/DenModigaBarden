using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/Enemy/SleepSFXStart")]
public class SleepSFXStart : StateAction {

    public Vector3 offset;

    public override void ActOnce(StateController controller) {
        EnemyData data = (EnemyData)controller.data;
        
        data.sleepSFXObject = Instantiate(data.sleepSFXPrefab, data.transform.position + offset, Quaternion.Euler(0, 0, 0), data.transform);
    }
}
