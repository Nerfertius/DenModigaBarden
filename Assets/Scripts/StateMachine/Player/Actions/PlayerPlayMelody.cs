using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/Player/PlayerPlayMelody")]
public class PlayerPlayMelody : StateAction {


    public override void Act(StateController controller) {
        PlayerData data = (PlayerData)controller.data;
        PlayerData.MelodyData mData = data.melodyData;

        foreach (Note note in mData.Notes) {
            if (Input.GetButtonDown(note.Button)) {
                mData.PlayedNotes.AddLast(note);

                data.audioSource.clip = note.audio;
                data.audioSource.Play();

                ParticleSystem m_fx = data.noteFX;
                ParticleSystem.TextureSheetAnimationModule m_anim = m_fx.textureSheetAnimation;
                Instantiate(m_fx, new Vector2(data.transform.position.x, data.spriteRenderer.bounds.max.y), Quaternion.Euler(data.noteFX.transform.rotation.eulerAngles));
                m_anim.rowIndex = note.FXRowNumber;
                m_fx.GetComponent<FXdestroyer>().hasPlayed = true;
            }
        }



        while (mData.PlayedNotes.Count > mData.MaxSavedNotes) {
            mData.PlayedNotes.RemoveFirst();
        }
    }
}
