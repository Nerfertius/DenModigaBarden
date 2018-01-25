using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/Player/PlayerClimbAction")]
public class PlayerClimbAction : StateAction
{
    public override void ActOnce(StateController controller)
    {
        PlayerData data = (PlayerData)controller.data;
        data.climbing = true;
        data.body.gravityScale = 0;
        data.body.velocity = Vector2.zero;
        if (data.transform.position.y > data.ladderTop.transform.position.y)
        {
            data.transform.position = new Vector2(data.ladderBottom.transform.position.x, data.ladderTop.transform.position.y - 0.5f);
        }
        else 
        {
            data.transform.position = new Vector2(data.ladderBottom.transform.position.x, data.transform.position.y);
        }
    }

    public override void FixedAct(StateController controller)
    {
        PlayerData data = (PlayerData)controller.data;
        data.moveVertical = Input.GetAxis("Vertical");
        data.transform.Translate(new Vector2(0, data.moveVertical) * data.climbSpeed * Time.deltaTime);
    }
}