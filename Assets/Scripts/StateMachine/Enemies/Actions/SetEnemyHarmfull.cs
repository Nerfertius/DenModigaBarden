using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/Enemy/SetEnemyHarmfull")]
public class SetEnemyHarmfull : StateAction {

    public bool setValue;

    public override void ActOnce(StateController controller) {
        EnemyData data = (EnemyData)controller.data;

        data.harmful = setValue;
    }
}
