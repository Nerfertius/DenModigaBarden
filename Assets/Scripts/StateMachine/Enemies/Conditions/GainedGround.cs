using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Condition/Enemy/GainedGround")]
public class GainedGround : Condition {


    private Vector3 sizeMult = new Vector3(0.8f, 1, 1);
    private Vector3 positionOffset = new Vector3(0, -0.2f, 0);

    public LayerMask layerMask;

    public override bool? CheckCollisionEnter(StateController controller, Collision2D coll) {
        Bounds bounds = coll.collider.bounds;
        
        return Physics2D.OverlapBox(controller.transform.position + positionOffset,
                            Vector3.Scale(bounds.size, sizeMult),
                            0,
                            layerMask);
    }

}
