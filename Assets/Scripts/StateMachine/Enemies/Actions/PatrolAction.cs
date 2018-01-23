using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/Enemy/PatrolAction")]
public class PatrolAction : StateAction {

    public LayerMask mask;

    public override void Act(StateController controller)
    {
        UpdateDirection(controller);

        if (NextToWall(controller)) {
            FlipDirection(controller);
        }

        Patrol(controller);
    }

    private void UpdateDirection(StateController controller)
    {
        if (controller.data.facingRight) {
            controller.data.currentDirection = new Vector2(1, 0);
            controller.transform.rotation = Quaternion.Euler(0, 0, 0);
        } else {
            controller.data.currentDirection = new Vector2(-1, 0);
            controller.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    private bool NextToWall(StateController controller)
    {
        RaycastHit2D hit = Physics2D.Raycast(controller.transform.position, controller.data.currentDirection, 0.525f, mask);

        return (hit);
    }

    private void FlipDirection(StateController controller)
    {
        controller.data.facingRight = !controller.data.facingRight;

        if (controller.data.facingRight) {
            controller.transform.rotation = Quaternion.Euler(0, 0, 0);
        } else {
            controller.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    private void Patrol(StateController controller)
    {
        EnemyData eData = (EnemyData)controller.data;
        controller.rb.velocity = controller.data.currentDirection * eData.speed * Time.deltaTime;
    }
}
