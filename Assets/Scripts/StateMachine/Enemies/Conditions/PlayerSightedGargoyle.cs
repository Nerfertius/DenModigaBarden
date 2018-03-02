using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Condition/Enemy/Gargoyle/PlayerSightedGargoyle")]
public class PlayerSightedGargoyle : Condition {

    public LayerMask mask;

    public LayerMask seeThroughWalls;

    public override bool? CheckCondition(StateController controller)
    {
        GargoyleData data = (GargoyleData)controller.data;

        if(data != null && data.sight.PlayerInRange()) {
            RaycastHit2D hit;

            if (data.CanSeeThrughWalls) {
                hit = Physics2D.Linecast(controller.transform.position, data.sight.player.transform.position, seeThroughWalls);
            }
            else {
                hit = Physics2D.Linecast(controller.transform.position, data.sight.player.transform.position, mask);
            }
            
            return (hit.collider.tag == "Player");
        }
        return false;        
    }
}
