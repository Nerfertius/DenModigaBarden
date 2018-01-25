using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/Enemy/ChaseAction")]
public class ChaseAction : StateAction
{
    public override void Act(StateController controller)
    {
        EnemyData eData = (EnemyData)controller.data;

        if (eData.player != null)
        {

            if (controller.transform.position.x < eData.player.position.x)
            {
                eData.currentDirection = new Vector2(1, 0);
            }
            else
            {
                eData.currentDirection = new Vector2(-1, 0);
            }

            Vector2 velocity = eData.currentDirection * eData.chaseSpeed * Time.deltaTime;
            controller.rb.MovePosition((Vector2)controller.transform.position + velocity);
        }
    }
}
