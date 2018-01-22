﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Condition/Enemy/PlayerSighted")]
public class PlayerSighted : Condition {
    public override bool? CheckCondition(StateController controller)
    {
        EnemyData eData = (EnemyData) controller.data;

        if (eData.player == null)
            return false;

        RaycastHit2D hit = Physics2D.Linecast(controller.transform.position, eData.player.position);

        return (hit.collider.tag == "Player");
    }
}
