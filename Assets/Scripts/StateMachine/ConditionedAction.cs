using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ConditionedAction{

    
    public Condition condition;
    public StateAction[] actions;

    private void DoActions(StateController controller) {
        foreach(StateAction action in actions) {
            action.ActOnce(controller);
        }
    }

    public void CheckCollisionEnter(StateController controller, Collision2D coll) {
        if(condition.CheckCollisionEnter(controller, coll) == true) {
            DoActions(controller);
        }
    }

    public void CheckCollisionExit(StateController controller, Collision2D coll) {
        if (condition.CheckCollisionExit(controller, coll) == true) {
            DoActions(controller);
        }
    }

    public void CheckCollisionStay(StateController controller, Collision2D coll) {
        if (condition.CheckCollisionStay(controller, coll) == true) {
            DoActions(controller);
        }
    }

    public void CheckTriggerEnter(StateController controller, Collider2D other) {
        if (condition.CheckTriggerEnter(controller, other) == true) {
            DoActions(controller);
        }
    }

    public void CheckTriggerExit(StateController controller, Collider2D other) {
        if (condition.CheckTriggerExit(controller, other) == true) {
            DoActions(controller);
        }
    }

    public void CheckTriggerStay(StateController controller, Collider2D other) {
        if (condition.CheckTriggerStay(controller, other) == true) {
            DoActions(controller);
        }
    }

    public void CheckCondition(StateController controller) {
        if (condition.CheckCondition(controller) == true) {
            DoActions(controller);
        }
    }
}
