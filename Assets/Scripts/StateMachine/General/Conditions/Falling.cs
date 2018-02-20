using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Condition/General/Falling")]

public class Falling : Condition
{
    public LayerMask mask;

    public override bool? CheckCondition(StateController controller)
    {
        return !(Physics2D.OverlapBox((Vector2)controller.transform.position - new Vector2(0, controller.coll.bounds.size.y / 5), (Vector2)controller.coll.bounds.size - new Vector2(0.1f,0), 0, mask));
    }
}