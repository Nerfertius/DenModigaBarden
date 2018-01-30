using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Condition/General/CollidedWith")]
public class CollidedWith : Condition {

    public LayerMask mask;

    public override bool? CheckCollisionEnter(StateController controller, Collision2D other)
    {
        return mask == (mask | (1 << other.gameObject.layer)); //supposedly checks if any layer in mask is layer
    }

}
