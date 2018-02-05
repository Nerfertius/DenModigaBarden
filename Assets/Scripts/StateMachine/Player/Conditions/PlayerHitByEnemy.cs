using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Condition/Player/PlayerHitByEnemy")]
public class PlayerHitByEnemy : Condition {

	public override bool? CheckTriggerStay(StateController controller, Collider2D coll) {
        PlayerData data = (PlayerData)controller.data;
        EnemyData dataE = coll.GetComponent<EnemyData>();

        if(dataE != null) {
            Debug.Log("isenemy " + coll.tag == "Enemy" && data.hitInvincibilityTimer.TimeUp() && dataE.harmful);
            Debug.Log("is not invincible " + data.hitInvincibilityTimer.TimeUp());
            Debug.Log("ishatmful " + dataE.harmful);
            if (coll.tag == "Enemy" && data.hitInvincibilityTimer.TimeUp() && dataE.harmful) {
                data.hitAngle = (data.transform.position - coll.transform.position).normalized;
                return true;
            }

            data.hitAngle = new Vector2(0, 0);
        }

       
        return false;
        
    }
}
