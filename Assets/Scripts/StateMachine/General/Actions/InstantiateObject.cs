using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/General/InstantiateObject")]
public class InstantiateObject : StateAction
{
    public GameObject gameObject;
    public Vector2 positionOffset;

    public override void ActOnce(StateController controller)
    {
        GameObject.Instantiate(gameObject, (Vector2)controller.transform.position + positionOffset, controller.transform.rotation);
    }

}
