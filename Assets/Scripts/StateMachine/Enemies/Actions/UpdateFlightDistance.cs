using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/Enemy/UpdateFlightDistance")]
public class UpdateFlightDistance : StateAction
{
    public override void Act(StateController controller)
    {
        FlyingEnemyData eData = (FlyingEnemyData)controller.data;

        eData.currentFlightDistance += eData.speed * Time.deltaTime;

        if (eData.currentFlightDistance >= eData.maxFlightDistance)
        {
            eData.facingRight = !eData.facingRight;
            eData.currentDirection.x *= -1;
            controller.transform.localScale = new Vector3(controller.transform.localScale.x * -1, 1, 1);
            eData.currentFlightDistance = 0;
        }
    }
}
