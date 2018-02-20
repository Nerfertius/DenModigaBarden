using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/General/PlayAudioClipOneShot")]
public class PlayAudioClipOneShot : StateAction {

    public AudioClip clip;

    public override void ActOnce(StateController controller) {
        AudioManager.Instance.PlayOneShot(clip);
    }
}
