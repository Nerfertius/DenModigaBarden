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

            data.rb.velocity = new Vector2(0, 0);

            data.health -= 0.5f;

            // take damage

            Vector2 knockbackDirection = new Vector2(1, 1).normalized;
            if(data.hitAngle.x < 0) {
                knockbackDirection = new Vector2(knockbackDirection.x * -1, knockbackDirection.y);
            }

            // only horizontal knockback
            /*Vector2 knockbackDirection = new Vector2(data.hitAngle.x, 0).normalized;
            data.rb.AddForce(knockbackDirection * knockbackForce * data.rb.mass);
            */
            //allow vertical knockback
            data.rb.AddForce(knockbackDirection * knockbackForce * data.rb.mass);

        }
    }
}
