using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Condition/General/TriggerEnterWithTag")]
public class TriggerEnterWithTag : Condition {
    public string tagName;
    public override bool? CheckTriggerEnter(StateController controller, Collider2D other)
    {
        return (other.tag == tagName);
    }

}
