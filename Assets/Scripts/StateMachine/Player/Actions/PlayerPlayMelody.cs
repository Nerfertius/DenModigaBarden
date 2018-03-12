using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/Player/PlayerPlayMelody")]
public class PlayerPlayMelody : StateAction {
    bool melodyAxisUp = true;

    public override void Act(StateController controller) {
        PlayerData data = (PlayerData)controller.data;
        PlayerData.MelodyData mData = data.melodyData;

        if (Input.GetButton("PlayMelody") || Input.GetAxisRaw("PlayMelody Dpad") > InputExtender.TriggerThreshold) {
            AudioManager.FadeBGM();
            mData.playingFlute = true;

            if (mData.currentMelody != null) {
                data.MelodyStoppedPlaying(mData.currentMelody);
            }
            mData.currentMelody = null;
            data.melodyData.MelodyRange.enabled = false;
            controller.anim.SetBool("Channeling", true);

            Note notePlayed = null;
            if (Input.GetButton("PlayMelodyNoteShift") || Input.GetAxisRaw("PlayMelodyNoteShift Dpad") > InputExtender.TriggerThreshold)
            {
                foreach (Note note in mData.Notes2) {
                    
                    if (note.CheckButton()) {
                        mData.PlayedNotes.AddLast(note);
                        notePlayed = note;
                    }
                }
            }
            else
            {
                foreach (Note note in mData.Notes1) {
                    if (note.CheckButton()) {
                        mData.PlayedNotes.AddLast(note);
                        notePlayed = note;
                    }
                }
            }
            if(notePlayed != null) {
                AudioManager.PlayNote(notePlayed.audio);

                ParticleSystem m_fx = data.noteFX;
                ParticleSystem.TextureSheetAnimationModule m_anim = m_fx.textureSheetAnimation;
                m_anim.rowIndex = notePlayed.FXRowNumber;
                Instantiate(m_fx, new Vector2(data.transform.position.x, data.collider.bounds.max.y), Quaternion.Euler(data.noteFX.transform.rotation.eulerAngles));
                m_fx.GetComponent<FXdestroyer>().hasPlayed = true;
            }
            
            while (mData.PlayedNotes.Count > mData.MaxSavedNotes) {
                mData.PlayedNotes.RemoveFirst();
            }
        }

        if (Input.GetButtonUp("PlayMelody") || InputExtender.GetAxisUp("PlayMelody Dpad")) {
            bool melodyPlayed = false;
            
            foreach (Melody melody in mData.melodies) {
                if (melody.CheckMelody(mData.PlayedNotes)) {
                    mData.currentMelody = melody.melodyID;
                    mData.MelodyRange.enabled = true;
                    melodyPlayed = true;

                    data.MelodyPlayed(melody.melodyID);

                    break;
                }
            }
            if (!melodyPlayed) {
                data.MelodyStoppedPlaying(mData.currentMelody);
                mData.currentMelody = null;
                mData.playingFlute = false;
                mData.MelodyRange.enabled = false;
                controller.anim.SetBool("Channeling", false);
            }
            AudioManager.FadeBGMBackToNormal();
            mData.PlayedNotes.Clear();
        }
    }
}
