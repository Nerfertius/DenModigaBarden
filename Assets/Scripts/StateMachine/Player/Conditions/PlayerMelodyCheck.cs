using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/Player/PlayerMelodyCheck")]
public class PlayerMelodyCheck : StateAction
{
    public override void Act(StateController controller)
    {
        PlayerData data = (PlayerData)controller.data;
        PlayerData.MelodyData mData = data.melodyData;

        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log(mData.currentMelody.ToString());
        }

        if (mData.currentMelody == null && !Input.GetButton("PlayMelody"))
        {
            controller.anim.SetBool("Channeling", false);
        } else if (mData.currentMelody != null || Input.GetButtonDown("PlayMelody"))
        {
            controller.anim.SetBool("Channeling", true);
        }
    }
}