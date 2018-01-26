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

        //Bottom
        if (data.transform.position.y < data.ladderBottom.y)
        {
            data.transform.position = new Vector2(data.ladderBottom.x, data.ladderBottom.y);
        }

        //Top
        if (data.transform.position.y > data.ladderTop.y)
        {
            data.transform.position = new Vector2(data.ladderBottom.x, data.ladderTop.y - 0.3f);
        }
        
        //Between
        else 
        {
            data.transform.position = new Vector2(data.ladderBottom.x, data.transform.position.y);
        }
    }

    public override void FixedAct(StateController controller)
    {
        PlayerData data = (PlayerData)controller.data;
        data.moveVertical = Input.GetAxis("Vertical");
        data.transform.Translate(new Vector2(0, data.moveVertical) * data.climbSpeed * Time.deltaTime);
    }
}