using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/Player/PlayerDirection")]
public class PlayerDirection : StateAction
{
    public override void FixedAct(StateController controller)
    {
        PlayerData data = (PlayerData)controller.data;
        if (Mathf.Abs(data.body.velocity.normalized.x) > 0)
        {
            if (data.body.velocity.normalized.x > 0)
            {
                data.transform.localScale = new Vector2(data.startScale.x, data.transform.localScale.y);
            }
            else if (data.body.velocity.normalized.x < 0)
            {
                data.transform.localScale = new Vector2(data.startScale.x * -1, data.transform.localScale.y);
            }
        }
    }
}