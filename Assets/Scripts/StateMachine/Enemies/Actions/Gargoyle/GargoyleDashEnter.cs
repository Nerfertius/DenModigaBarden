using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/Enemy/Gargoyle/GargoyleDashEnter")]
public class GargoyleDashEnter : StateAction {

    public float JumpForce = 350;
    public float DashForce = 500;

    private Vector2 jumpAngle = new Vector2(0, 1);
    private Vector2 dashAngle = new Vector2(1, 0);


    public override void ActOnce(StateController controller) {
        GargoyleData data = (GargoyleData)controller.data;

        controller.rb.velocity = new Vector2(0, 0);
        controller.rb.AddForce(jumpAngle * JumpForce * controller.rb.mass);
        controller.rb.AddForce(dashAngle * controller.transform.localScale.x * DashForce * controller.rb.mass);

        data.dashCollider.enabled = true;
        data.idleCollider.enabled = false;
        data.transform.Translate(new Vector3(0.8f * data.transform.localScale.x, 0, 0));
    }
}