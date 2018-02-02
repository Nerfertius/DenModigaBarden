using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Condition/Enemy/PlayerSightedAV")]
public class PlayerSightedAV : Condition {

    public LayerMask mask;

    public override bool? CheckCondition(StateController controller)
    {
        EnemyData data = (EnemyData)controller.data;

        if(data != null && data.sight.PlayerInRange()) {
            RaycastHit2D hit;
            hit = Physics2D.Linecast(controller.transform.position, data.sight.player.transform.position, mask);
            return (hit.collider.tag == "Player");
        }
        return false;        
    }
}
