using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/Enemy/EvilEyeFlight")]
public class EvilEyeFlight : StateAction
{
    public override void Act(StateController controller)
    {
        FlyingEnemyData eData = (FlyingEnemyData)controller.data;

        controller.transform.position += new Vector3(eData.currentDirection.x * eData.speed * Time.deltaTime, Mathf.Sin((Time.time + eData.randomSineOffset) * eData.frequency) * eData.magnitude, controller.transform.position.z);
    }
}
