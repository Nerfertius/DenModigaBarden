using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Transition {
	public Condition condition;
	public State trueState;
	public State falseState;
    public StateAction[] trueStateTransitionActions;
    public StateAction[] falseStateTransitionActions;
}
