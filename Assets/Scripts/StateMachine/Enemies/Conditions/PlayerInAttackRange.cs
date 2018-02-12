using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = ("StateMachine/Condition/Enemy/PlayerInAttackRange"))]
public class PlayerInAttackRange : Condition {
	public override bool? CheckCondition (StateController controller)
	{
		EnemyData data = (EnemyData)controller.data;

		if (data.player == null) {
			return null;
		}

		return (Vector2.Distance (controller.transform.position, data.player.position) < data.attackRange);
	}
}
