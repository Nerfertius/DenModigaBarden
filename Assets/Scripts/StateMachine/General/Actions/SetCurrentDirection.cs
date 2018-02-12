using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/General/SetCurrentDirection")]
public class SetCurrentDirection : StateAction
{
    [Range(-1, 1)] public int x;
    [Range(-1, 1)] public int y;

    public bool setAuto;
    public override void ActOnce(StateController controller)
    {
        controller.data.currentDirection = new Vector2(x, y);

        if (setAuto)
        {
            controller.data.facingRight = controller.sprRend.flipX;

            if (controller.data.facingRight)
            {
                controller.data.currentDirection.x = 1 * controller.transform.localScale.x;
            }
            else
            {
                controller.data.currentDirection.x = -1 * controller.transform.localScale.x;
            }
        } else
        {

        }
    }
}
