using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/Enemy/HeavyEnemyJump")]
[System.Serializable]
public class HeavyEnemyJump : StateAction {

    public float JumpForce = 350;

    private Vector2 angle = new Vector2(0, 1);

    public override void ActOnce(StateController controller) {
        controller.rb.AddForce(angle * JumpForce);
    }

}
