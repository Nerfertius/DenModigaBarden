using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/Player/PlayerTakeDamage")]
public class PlayerTakeDamage : StateAction {

    public float knockbackForce;

    public override void ActOnce(StateController controller) {
        PlayerData data = (PlayerData)controller.data;

        if (data.hitInvincibilityTimer.IsDone()) {
            data.hitInvincibilityTimer.Start();
            if (data.lastDamageData != null) {
                // take damage
                if (data.lastDamageData.isMagical && data.magicShieldHealth > 0) {
                    data.magicShieldHealth -= data.lastDamageData.damage;
                    //Debug.Log("ShieldHealth: " + data.magicShieldHealth);
                }
                else {
                    data.health -= data.lastDamageData.damage;
                    data.CancelPlayingMelody();
                    //Debug.Log("Health: " + data.health);
                }

                data.rb.velocity = new Vector2(0, 0);
                Vector2 knockbackDirection = new Vector2(1, 1).normalized;
                
                if (data.hitAngle.x < 0) {
                    
                    knockbackDirection = new Vector2(knockbackDirection.x * -1, knockbackDirection.y);
                }

                data.rb.AddForce(knockbackDirection * data.lastDamageData.knockbackPower * data.rb.mass);
                data.lastDamageData = null;
            }
        }
    }
}
