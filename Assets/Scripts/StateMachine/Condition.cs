using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Condition : ScriptableObject {
	public virtual bool? CheckCondition (StateController controller){return null;}
	public virtual bool? CheckCollisionEnter(StateController controller, Collision2D coll){return null; }
    public virtual bool? CheckCollisionExit(StateController controller, Collision2D coll) { return null; }
    public virtual bool? CheckCollisionStay(StateController controller, Collision2D coll){return null;}
    public virtual bool? CheckTriggerEnter(StateController controller, Collider2D other) { return null; }
    public virtual bool? CheckTriggerExit(StateController controller, Collider2D other) { return null; }
    public virtual bool? CheckTriggerStay(StateController controller, Collider2D other) { return null; }
}
