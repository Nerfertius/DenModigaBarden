using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Condition/Player/PlayerHitByEnemy")]
public class PlayerHitByEnemy : Condition {

	public override bool? CheckTriggerStay(StateController controller, Collider2D coll) {
        PlayerData data = (PlayerData)controller.data;
        EnemyData dataE = coll.GetComponent<EnemyData>();

        if(dataE != null) {
            if ((coll.tag == "Enemy" || coll.tag == "Hitbox") && data.hitInvincibilityTimer.TimeUp() && dataE.harmful) {
                data.hitAngle = (data.transform.position - coll.transform.position).normalized;
                if(data.hitAngle.magnitude == 0) {
                    data.hitAngle = new Vector2(1, 1).normalized;
                }
                return true;
            }

            data.hitAngle = new Vector2(0, 0);
        }

       
        return false;
        
    }
}
