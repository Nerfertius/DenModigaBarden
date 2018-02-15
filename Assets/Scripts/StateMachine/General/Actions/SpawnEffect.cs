using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/General/SpawnEffect")]
public class SpawnEffect : StateAction
{
    public GameObject obj;
    public Vector3 offset;

    public override void ActOnce(StateController controller)
    {
        Instantiate(obj, controller.transform.position + offset, Quaternion.identity);
    }
}
