using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Condition/Enemy/PlayerSighted")]
public class PlayerSighted : Condition {

    public LayerMask mask;

    public override bool? CheckCondition(StateController controller)
    {
        EnemyData eData = (EnemyData) controller.data;

        if (eData.player == null) { 
            return false;
        }
        else if (eData.currentDirection.x == 1 && eData.player.position.x < controller.transform.position.x){
            return false;
        }
        else if (eData.currentDirection.x == -1 && eData.player.position.x > controller.transform.position.x){
            return false;
        } else if (controller.transform.position.y + 2 < eData.player.position.y){
            return false;
        }

        RaycastHit2D hit;
        if (controller.transform.position.y < eData.player.position.y - 0.3f)
        {
            hit = Physics2D.Linecast(controller.transform.position, eData.player.position, mask);
        } else
        {
            hit = Physics2D.Linecast(controller.transform.position, eData.player.position, mask, -1);
        }
        
        return (hit.collider.tag == "Player");
    }
}
