﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/Enemy/PatrolAction")]
public class PatrolAction : StateAction {

    public LayerMask mask;

    public override void Act(StateController controller)
    {
        UpdateDirection(controller);

        if (NextToWall(controller) || OnEdge(controller)) {
            FlipDirection(controller);
        }

        Patrol(controller);
    }

    private void UpdateDirection(StateController controller)
    {
        if (controller.data.facingRight) {
            controller.data.currentDirection = new Vector2(1, 0);
        } else {
            controller.data.currentDirection = new Vector2(-1, 0);
        }

        controller.sprRend.flipX = controller.data.facingRight;
    }

    private bool NextToWall(StateController controller)
    {
        float xOffset = controller.coll.bounds.size.x * 0.6f;
        float yOffset = controller.coll.bounds.size.y * 0.5f;
        RaycastHit2D hit = Physics2D.Raycast(controller.transform.position - new Vector3(0, yOffset, 0), controller.data.currentDirection, xOffset, mask);
        
        if(hit.collider != null && hit.collider.tag != "Ground")
        {
            return false;
        }

        return (hit);
    }

    private bool OnEdge(StateController controller)
    {
        Vector2 offset;

        if (controller.sprRend.flipX) {
            offset = new Vector2(controller.coll.bounds.size.x * 0.5f, 0);
        } else {
            offset = new Vector2(-controller.coll.bounds.size.x * 0.5f, 0);
        }

        RaycastHit2D hit = Physics2D.Raycast((Vector2)controller.transform.position + offset, Vector2.down, controller.coll.bounds.size.y, mask);
        Debug.DrawRay(controller.transform.position + (Vector3)offset, Vector3.down, Color.red, 0.1f);

        return (hit.collider == null);
    }

    private void FlipDirection(StateController controller)
    {
        controller.data.facingRight = !controller.data.facingRight;
        controller.sprRend.flipX = controller.data.facingRight;
    }

    private void Patrol(StateController controller)
    {
        EnemyData eData = (EnemyData)controller.data;
        Vector2 velocity = controller.data.currentDirection * eData.speed * Time.fixedDeltaTime;
        controller.rb.MovePosition((Vector2)controller.transform.position + velocity);
    }
}
