using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "StateMachine/State")]
public class State : ScriptableObject {

	public StateAction[] entryActions;
	public StateAction[] exitActions;
	public StateAction[] actions;
	public Transition[] transitions;

	public bool hasExitTime;
	public float exitTimer;
	public bool useRandomTimer;
	public float minTime;
	public float maxTime;
	public State nextState;

	public void UpdateState (StateController controller)
	{
		DoActions (controller);
		CheckTransitions (controller);

		if (hasExitTime) {
			CheckExitTimer(controller);
		}
	}

	private void CheckExitTimer (StateController controller)
	{
		controller.stateTimer -= Time.deltaTime;
		if (controller.stateTimer <= 0) {
			controller.TransitionToState(nextState);
		}
	}

	public void FixedUpdateState (StateController controller)
	{
		DoFixedActions(controller);
	}

	private void DoActions (StateController controller)
	{
		for (int i = 0; i < actions.Length; i++) {
			actions[i].Act(controller);
		}
	}

	private void DoFixedActions (StateController controller)
	{
		for (int i = 0; i < actions.Length; i++) {
			actions[i].FixedAct(controller);
		}
	}

	public void DoEntryActions (StateController controller)
    {
        for (int i = 0; i < entryActions.Length; i++)
        {
            entryActions[i].ActOnce(controller);
        }
	}

	public void DoExitActions (StateController controller)
	{
        for (int i = 0; i < exitActions.Length; i++)
        {
            exitActions[i].ActOnce(controller);
        }
    }

	public void CheckCollisionEnter (StateController controller, Collision2D coll)
	{
		for (int i = 0; i < transitions.Length; i++) {
			bool? condition = transitions[i].condition.CheckCollisionEnter(controller, coll);
			SendTransitionMessage(controller, i, condition);
		}
	}

    public void CheckCollisionExit(StateController controller, Collision2D coll)
    {
        for (int i = 0; i < transitions.Length; i++)
        {
            bool? condition = transitions[i].condition.CheckCollisionExit(controller, coll);
            SendTransitionMessage(controller, i, condition);
        }
    }

    public void CheckCollisionStay (StateController controller, Collision2D coll)
	{
		for (int i = 0; i < transitions.Length; i++) {
			bool? condition = transitions[i].condition.CheckCollisionStay(controller, coll);
			SendTransitionMessage(controller, i, condition);
		}
	}

    public void CheckTriggerEnter(StateController controller, Collider2D other)
    {
        for (int i = 0; i < transitions.Length; i++)
        {
            bool? condition = transitions[i].condition.CheckTriggerEnter(controller, other);
            SendTransitionMessage(controller, i, condition);
        }
    }

    public void CheckTriggerExit(StateController controller, Collider2D other)
    {
        for (int i = 0; i < transitions.Length; i++)
        {
            bool? condition = transitions[i].condition.CheckTriggerExit(controller, other);
            SendTransitionMessage(controller, i, condition);
        }
    }

    public void CheckTriggerStay(StateController controller, Collider2D other)
    {
        for (int i = 0; i < transitions.Length; i++)
        {
            bool? condition = transitions[i].condition.CheckTriggerStay(controller, other);
            SendTransitionMessage(controller, i, condition);
        }
    }

    private void CheckTransitions (StateController controller)
	{
		for (int i = 0; i < transitions.Length; i++) {
			bool? condition = transitions[i].condition.CheckCondition(controller);
			SendTransitionMessage(controller, i, condition);
		}
	}

	private void SendTransitionMessage (StateController controller, int arrayNumber, bool? condition)
	{
		if (condition.HasValue) {
			if ((bool)condition) {
				controller.TransitionToState (transitions [arrayNumber].trueState);
			} else {
				controller.TransitionToState (transitions [arrayNumber].falseState);
			}
		}
	}

}
