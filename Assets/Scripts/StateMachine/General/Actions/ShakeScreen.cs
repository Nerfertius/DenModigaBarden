using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/General/ShakeScreen")]
public class ShakeScreen : StateAction
{
    public float duration;
    public float xIntensity;
    public float yIntensity;

    public override void ActOnce(StateController controller)
    {
        CameraFX.Screenshake(duration, xIntensity, yIntensity);
    }
}
