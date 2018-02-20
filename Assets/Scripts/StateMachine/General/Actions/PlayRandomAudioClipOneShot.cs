using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/General/PlayRandomAudioClipOneShot")]
public class PlayRandomAudioClipOneShot : StateAction {

    public AudioClip[] clips;

    public override void ActOnce(StateController controller) {
        AudioManager.Instance.PlayOneShot(clips[Random.Range(0, clips.Length - 1)]);
    }
}
