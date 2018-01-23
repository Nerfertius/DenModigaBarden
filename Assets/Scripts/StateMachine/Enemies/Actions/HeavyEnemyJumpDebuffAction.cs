using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/Enemies/HeavyEnemyJumpDebuffActionEntry")]
public class HeavyEnemyJumpDebuffActionEntry : StateAction {

    public float JumpForce = 350;

    private Vector2 angle = new Vector2(0, 1);

    public override void ActOnce(StateController controller) {
        controller.GetComponent<Rigidbody2D>().AddForce(angle * JumpForce);
    }

}
