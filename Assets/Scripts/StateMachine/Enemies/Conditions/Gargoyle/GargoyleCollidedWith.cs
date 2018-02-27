using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Condition/Enemy/Gargoyle/GargoyleCollidedWith")]
public class GargoyleCollidedWith : Condition {

    public LayerMask mask;

    public override bool? CheckCollisionEnter(StateController controller, Collision2D other)
    {
        if (mask == (mask | (1 << other.gameObject.layer))) {
            
            if (other.transform.tag == "Breakable") {
                GargoyleData data = (GargoyleData)controller.data;
                data.rb.velocity = data.previousVelocity;
                
                return false;
            }
            return true;
        }
        return false; 
    }
}
