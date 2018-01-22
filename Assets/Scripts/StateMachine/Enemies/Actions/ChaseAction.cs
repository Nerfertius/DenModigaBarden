using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/Enemy/ChaseAction")]
public class ChaseAction : StateAction
{
    public override void Act(StateController controller)
    {
        EnemyData eData = (EnemyData)controller.data;

        if(eData.player != null)
        {
            if (controller.transform.position.x < eData.player.position.x)
            {
                controller.rb.velocity = new Vector2(eData.chaseSpeed, controller.rb.velocity.y) * Time.deltaTime;
            }
            else
            {
                controller.rb.velocity = new Vector2(-eData.chaseSpeed, controller.rb.velocity.y) * Time.deltaTime;
            }
        }
    }
}
