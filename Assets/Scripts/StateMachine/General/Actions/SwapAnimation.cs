using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/General/SwapAnimation")]
public class SwapAnimation : StateAction
    {

    public string animationToSwapOut;
    public string animationToSwapIn;

    public override void ActOnce(StateController controller) {
        base.ActOnce(controller);
        controller.StartCoroutine(swapOnce(controller));
    }

    private IEnumerator swapOnce(StateController controller) {
        yield return new WaitForEndOfFrame();
        if (!controller.anim.GetCurrentAnimatorStateInfo(0).IsName(animationToSwapOut)) {
            controller.anim.Play(animationToSwapIn);
        }
    }


    public override void Act(StateController controller) {
        if (animationDone(controller)) {
            controller.anim.Play(animationToSwapIn);
        }
    }

    private bool animationDone(StateController controller) {
        return controller.anim.GetCurrentAnimatorStateInfo(0).IsName(animationToSwapOut) &&  // is it the right animation
            controller.anim.GetCurrentAnimatorStateInfo(0).length <
            controller.anim.GetCurrentAnimatorStateInfo(0).normalizedTime; // is it done
    }
}
