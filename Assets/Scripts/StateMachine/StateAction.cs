using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateAction : ScriptableObject {
	public virtual void Act (StateController controller){}
	public virtual void FixedAct (StateController controller){}
	public virtual void ActOnce (StateController controller){}
}
