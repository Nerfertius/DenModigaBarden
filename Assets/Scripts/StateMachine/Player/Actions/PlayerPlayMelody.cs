using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/Player/PlayerPlayMelody")]
public class PlayerPlayMelody : StateAction {


    public override void Act(StateController controller) {
        PlayerData data = (PlayerData)controller.data;
        PlayerData.MelodyData mData = data.melodyData;

        if (Input.GetButton("PlayMelody")) {
            mData.playingFlute = true;
            mData.currentMelody = null;
            controller.anim.SetBool("Channeling", true);

            foreach (Note note in mData.Notes) {
                if (Input.GetButtonDown(note.Button)) {
                    mData.PlayedNotes.AddLast(note);

                    if (Input.GetButton("HighPitch")) {
                        data.audioSource.pitch = mData.highPitchValue;
                    }
                    else if (Input.GetButton("LowPitch")) {
                        data.audioSource.pitch = mData.lowPitchValue;
                    }
                    else {
                        data.audioSource.pitch = mData.standardPitchValue;
                    }
                    data.PlaySound(note.audio);

                    ParticleSystem m_fx = data.noteFX;
                    ParticleSystem.TextureSheetAnimationModule m_anim = m_fx.textureSheetAnimation;
                    m_anim.rowIndex = note.FXRowNumber;
                    Instantiate(m_fx, new Vector2(data.transform.position.x, data.spriteRenderer.bounds.max.y), Quaternion.Euler(data.noteFX.transform.rotation.eulerAngles));
                    m_fx.GetComponent<FXdestroyer>().hasPlayed = true;
                }
            }
            while (mData.PlayedNotes.Count > mData.MaxSavedNotes) {
                mData.PlayedNotes.RemoveFirst();
            }
        }


        if (Input.GetButtonUp("PlayMelody")) {
            bool melodyPlayed = false;
            foreach (Melody melody in mData.melodies) {
                if (melody.CheckMelody(mData.PlayedNotes)) {
                    mData.currentMelody = melody.melodyID;
                    mData.MelodyRange.enabled = true;
                    melodyPlayed = true;
                    break;
                }
            }
            if (!melodyPlayed) {
                mData.currentMelody = null;
                mData.MelodyRange.enabled = false;
                controller.anim.SetBool("Channeling", false);
            }
            mData.PlayedNotes.Clear();
        }


    }
}
