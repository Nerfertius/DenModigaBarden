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
                eData.facingRight = true;
                controller.sprRend.flipX = true;
            }
            else
            {
                eData.currentDirection = new Vector2(-1, 0);
                eData.facingRight = false;
                controller.sprRend.flipX = false;
            }

            float distance = Vector2.Distance(controller.transform.position, eData.player.position);
            if (distance > eData.attackRange - 0.1f)
            {
                Vector2 velocity = eData.currentDirection * eData.chaseSpeed * Time.fixedDeltaTime;
                controller.rb.MovePosition((Vector2)controller.transform.position + velocity);
            }
        }
    }
}
