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

        Physics2D.IgnoreLayerCollision(data.playerLayer, data.climbFixLayer, true);

        //Bottom
        if (data.transform.position.y < data.ladderBottom.position.y)
        {
            data.transform.position = new Vector2(data.ladderBottom.position.x, data.ladderBottom.position.y);
        }

        //Top
        if (data.transform.position.y > data.ladderTop.position.y)
        {
            data.transform.position = new Vector2(data.ladderBottom.position.x, data.ladderTop.position.y);
        }
        
        //Between
        else 
        {
            data.transform.position = new Vector2(data.ladderBottom.position.x, data.transform.position.y);
        }
    }

    public override void FixedAct(StateController controller)
    {
        PlayerData data = (PlayerData)controller.data;
        data.moveVertical = Input.GetAxis("Vertical");
        data.transform.Translate(new Vector2(0, data.moveVertical) * data.climbSpeed * Time.deltaTime);
    }
}