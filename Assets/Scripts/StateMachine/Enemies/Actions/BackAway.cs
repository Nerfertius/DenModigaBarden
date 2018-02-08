using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = ("StateMachine/Action/Enemy/BackAway"))]
public class BackAway : StateAction {
    public override void Act(StateController controller)
    {
        EnemyData eData = (EnemyData)controller.data;

        Vector2 velocity = -eData.currentDirection * eData.speed * 0.5f * Time.fixedDeltaTime;
        controller.rb.MovePosition((Vector2)controller.transform.position + velocity);
    }
}
