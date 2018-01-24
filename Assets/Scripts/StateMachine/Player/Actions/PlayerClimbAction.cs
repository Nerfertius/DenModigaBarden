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
    }

    public override void FixedAct(StateController controller)
    {
        PlayerData data = (PlayerData)controller.data;
        data.moveVertical = Input.GetAxis("Vertical");
        data.transform.Translate(new Vector2(0, data.moveVertical) * data.climbSpeed * Time.deltaTime);
    }
}