using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/Enemy/EnterBattleSystem")]
public class EnterBattleSystem : StateAction
{
    public override void ActOnce(StateController controller)
    {
        EnemyData eData = (EnemyData)controller.data;

        if (eData != null)
        {
            GameManager.instance.switchState(new BattleState(GameManager.instance));
        }
    }

}
