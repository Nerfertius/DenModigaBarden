using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/Enemy/RemoveTarget")]
public class RemoveTarget : StateAction
{
    public override void ActOnce(StateController controller)
    {
        EnemyData eData = (EnemyData)controller.data;

        eData.player = null;
    }
}
