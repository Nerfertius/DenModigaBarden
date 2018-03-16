using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Condition/Enemy/TriggerEnterWithPlayer")]
public class TriggerEnterWithPlayer : Condition
{
    public override bool? CheckTriggerEnter(StateController controller, Collider2D other)
    {
        EnemyData data = (EnemyData) controller.data;
        if (data.childCollided)
        {
            data.childCollided = false;
            return null;
        }

        return (other.tag == "Player");
    }

}
