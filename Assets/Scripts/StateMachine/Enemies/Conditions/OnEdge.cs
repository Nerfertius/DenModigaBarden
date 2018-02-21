using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Condition/Enemy/OnEdge")]
public class OnEdge : Condition
{
    public LayerMask mask;
    public override bool? CheckCondition(StateController controller)
    {
        Vector2 offset;

        if (controller.sprRend.flipX)
        {
            offset = new Vector2(controller.coll.bounds.size.x * 0.5f, 0);
        }
        else
        {
            offset = new Vector2(-controller.coll.bounds.size.x * 0.5f, 0);
        }

        RaycastHit2D hit = Physics2D.Raycast((Vector2)controller.transform.position + offset, Vector2.down, controller.coll.bounds.size.y, mask);
        Debug.DrawRay(controller.transform.position + (Vector3)offset, Vector3.down, Color.red, 0.1f);

        return (hit.collider == null);
    }
}
