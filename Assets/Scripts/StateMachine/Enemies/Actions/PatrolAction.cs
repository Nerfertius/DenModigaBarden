using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/Enemies/PatrolAction")]
public class PatrolAction : StateAction {
    public override void Act(StateController controller)
    {
        UpdateDirection(controller);

        if (NextToWall(controller))
        {
            FlipDirection(controller);
        }

        Patrol(controller);
    }

    private void UpdateDirection(StateController controller)
    {
        if (controller.data.facingRight)
        {
            controller.data.currentDirection = new Vector2(1, 0);
        }
        else
        {
            controller.data.currentDirection = new Vector2(-1, 0);
        }
    }

    private bool NextToWall(StateController controller)
    {
        RaycastHit2D hit = Physics2D.Raycast(controller.transform.position, controller.data.currentDirection, 0.5f);

        return (hit);
    }

    private void FlipDirection(StateController controller)
    {
        controller.sprRend.flipX = !controller.sprRend.flipX;
        controller.data.facingRight = !controller.data.facingRight;
    }

    private void Patrol(StateController controller)
    {
        EnemyData eData = (EnemyData)controller.data;
        controller.rb.velocity = controller.data.currentDirection * eData.speed * Time.deltaTime;
    }
}
