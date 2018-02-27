using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Condition/Player/PlayerHitByEnemy")]
public class PlayerHitByEnemy : Condition {

	public override bool? CheckTriggerStay(StateController controller, Collider2D coll) {
        PlayerData data = (PlayerData)controller.data;
        EnemyData dataE = coll.GetComponent<EnemyData>();

        //TODO : Separate Trap & Enemy/Hitbox

        if (data.hitInvincibilityTimer.IsDone() && (coll.tag == "Hitbox" || coll.tag == "Trap" || (coll.tag == "Enemy" && dataE != null && dataE.harmful))) {
            data.lastDamageData = coll.GetComponent<PlayerDamageData>();
            data.hitAngle = (data.transform.position - coll.transform.position).normalized;
            if(data.hitAngle.magnitude == 0) {
                data.hitAngle = new Vector2(1, 1).normalized;
            }
            return true;
        }
        return false;        
    }
}
