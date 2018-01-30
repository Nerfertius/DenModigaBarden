using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "StateMachine/Action/General/PlayAnimation")]
public class PlayAnimation : StateAction {
    public string animationName;

    public override void ActOnce(StateController controller) {
        controller.anim.Play(animationName);
    }

}
