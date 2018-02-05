using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/Player/PlayerTakeDamage")]
public class PlayerTakeDamage : StateAction {

	public override void ActOnce(StateController controller) {
        PlayerData data = (PlayerData)controller.data;

        if (data.hitInvincibilityTimer.TimeUp()) {
            data.hitInvincibilityTimer.StartTimer();

            // take damage
            Vector2 knockbackDirection = new Vector2(data.hitAngle.x, 0).normalized;
            data.rb.AddForce(knockbackDirection * 600 * data.rb.mass);
        }
    }
}
