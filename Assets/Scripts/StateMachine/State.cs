using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "StateMachine/State")]
public class State : ScriptableObject {

	public StateAction[] entryActions;
	public StateAction[] exitActions;
	public StateAction[] actions;
	public Transition[] transitions;
    public ConditionedAction[] conditionedActions = new ConditionedAction[0];

	public bool hasExitTime;
	public float exitTimer;
	public bool useRandomTimer;
	public float minTime;
	public float maxTime;
	public State nextState;
    public StateAction[] timeOutActions;

	public void UpdateState (StateController controller)
	{
		DoActions (controller);
        CheckConditionedActions(controller);
		CheckTransitions (controller);

		if (hasExitTime) {
			CheckExitTimer(controller);
		}
	}

	private void CheckExitTimer (StateController controller)
	{
		controller.stateTimer -= Time.deltaTime;
		if (controller.stateTimer <= 0) {
			controller.TransitionToState(nextState, this, timeOutActions);
		}
	}

	public void FixedUpdateState (StateController controller)
	{
		DoFixedActions(controller);
	}

	private void DoActions (StateController controller)
	{
		for (int i = 0; i < actions.Length; i++) {
            if(actions[i] == null)
            {
                Debug.LogError(this + " is missing an action. Make sure no actions are null!");
                return;
            }
			actions[i].Act(controller);
		}
	}

	private void DoFixedActions (StateController controller)
	{
		for (int i = 0; i < actions.Length; i++) {
            if (actions[i] == null)
            {
                Debug.LogError(this + " is missing an action. Make sure no actions are null!");
                return;
            }
            actions[i].FixedAct(controller);
		}
	}

	public void DoEntryActions (StateController controller)
    {
        for (int i = 0; i < entryActions.Length; i++)
        {
            if (entryActions[i] == null)
            {
                Debug.LogError(this + " is missing an entry action. Make sure no actions are null!");
                return;
            }
            entryActions[i].ActOnce(controller);
        }
	}

	public void DoExitActions (StateController controller)
	{
        for (int i = 0; i < exitActions.Length; i++)
        {
            if (exitActions[i] == null)
            {
                Debug.LogError(this + " is missing an exit action. Make sure no actions are null!");
                return;
            }
            exitActions[i].ActOnce(controller);
        }
    }

	public void CheckCollisionEnter (StateController controller, Collision2D coll)
	{
		for (int i = 0; i < transitions.Length; i++) {
			bool? condition = transitions[i].condition.CheckCollisionEnter(controller, coll);
			SendTransitionMessage(controller, i, condition);
		}
        foreach (ConditionedAction conditionedAction in conditionedActions) {
            conditionedAction.CheckCollisionEnter(controller, coll);
        }
    }

    public void CheckCollisionExit(StateController controller, Collision2D coll)
    {
        for (int i = 0; i < transitions.Length; i++)
        {
            bool? condition = transitions[i].condition.CheckCollisionExit(controller, coll);
            SendTransitionMessage(controller, i, condition);
        }
        foreach (ConditionedAction conditionedAction in conditionedActions) {
            conditionedAction.CheckCollisionExit(controller, coll);
        }
    }

    public void CheckCollisionStay (StateController controller, Collision2D coll)
	{
		for (int i = 0; i < transitions.Length; i++) {
			bool? condition = transitions[i].condition.CheckCollisionStay(controller, coll);
			SendTransitionMessage(controller, i, condition);
		}
        foreach (ConditionedAction conditionedAction in conditionedActions) {
            conditionedAction.CheckCollisionStay(controller, coll);
        }
    }

    public void CheckTriggerEnter(StateController controller, Collider2D other)
    {
        for (int i = 0; i < transitions.Length; i++)
        {
            bool? condition = transitions[i].condition.CheckTriggerEnter(controller, other);
            SendTransitionMessage(controller, i, condition);
        }
        foreach (ConditionedAction conditionedAction in conditionedActions) {
            conditionedAction.CheckTriggerEnter(controller, other);
        }
    }

    public void CheckTriggerExit(StateController controller, Collider2D other)
    {
        for (int i = 0; i < transitions.Length; i++)
        {
            bool? condition = transitions[i].condition.CheckTriggerExit(controller, other);
            SendTransitionMessage(controller, i, condition);
        }
        foreach (ConditionedAction conditionedAction in conditionedActions) {
            conditionedAction.CheckTriggerExit(controller, other);
        }
    }

    public void CheckTriggerStay(StateController controller, Collider2D other)
    {
        for (int i = 0; i < transitions.Length; i++)
        {
            bool? condition = transitions[i].condition.CheckTriggerStay(controller, other);
            SendTransitionMessage(controller, i, condition);
        }
        foreach (ConditionedAction conditionedAction in conditionedActions) {
            conditionedAction.CheckTriggerStay(controller, other);
        }
    }

    private void CheckTransitions (StateController controller)
	{
		for (int i = 0; i < transitions.Length; i++) {
			bool? condition = transitions[i].condition.CheckCondition(controller);
			SendTransitionMessage(controller, i, condition);
		}
	}

    private void CheckConditionedActions(StateController controller) {
        foreach (ConditionedAction conditionedAction in conditionedActions) {
            conditionedAction.CheckCondition(controller);
        }
    }

    private void SendTransitionMessage (StateController controller, int arrayNumber, bool? condition)
	{
		if (condition.HasValue) {
			if ((bool)condition) {
				controller.TransitionToState (transitions [arrayNumber].trueState, this, transitions[arrayNumber].trueStateTransitionActions);
			} else {
				controller.TransitionToState (transitions [arrayNumber].falseState, this, transitions[arrayNumber].falseStateTransitionActions);
			}
		}
	}
}
