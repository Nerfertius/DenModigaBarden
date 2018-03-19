using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/Player/PlayerPlayMelody")]
public class PlayerPlayMelody : StateAction
{

    public delegate void PlayedMagicResistEventHandler();
    public static event PlayedMagicResistEventHandler PlayedMagicResist;

    public delegate void StoppedPlayingEventHandler();
    public static event StoppedPlayingEventHandler StoppedPlaying;

    public override void Act(StateController controller)
    {
        PlayerData data = (PlayerData)controller.data;
        PlayerData.MelodyData mData = data.melodyData;

        if(Input.GetButtonDown("PlayMelody") || InputExtender.GetAxisDown("PlayMelody Trigger")) {
            mData.playMelodyState = !mData.playMelodyState;

            // so you don't go to play melody when canceling a melody
            if(mData.currentMelody != null) {
                mData.playMelodyState = false;
                mData.playingFlute = false;

                data.MelodyStoppedPlaying(mData.currentMelody);
                if (mData.currentMelody == Melody.MelodyID.MagicResistMelody) {
                    StoppedPlaying();
                }
                mData.currentMelody = null;
                data.melodyData.MelodyRange.enabled = false;
                controller.anim.SetBool("Channeling", false);
            }

            else if (mData.playMelodyState) { // on start reading input
                AudioManager.FadeBGM();
                mData.playingFlute = true;


                if (mData.currentMelody != null) {
                    data.MelodyStoppedPlaying(mData.currentMelody);
                    if (mData.currentMelody == Melody.MelodyID.MagicResistMelody) {
                        StoppedPlaying();
                    }
                }
                mData.currentMelody = null;
                data.melodyData.MelodyRange.enabled = false;
                controller.anim.SetBool("Channeling", true);
            }
            else { // on cancel playing
                AudioManager.FadeBGMBackToNormal();
                mData.playingFlute = false;
                controller.anim.SetBool("Channeling", false);
            }
        }

        if (mData.playMelodyState) {
        // checks if a note is played
            Note notePlayed = null;
            //foreach (Note note in mData.Notes2) {
            //    if (note.CheckButton()) {
            //        mData.PlayedNotes.AddLast(note);
            //        notePlayed = note;
            //    }
            //}
            foreach (Note note in mData.Notes1) {
                if (note.CheckButton()) {
                    mData.PlayedNotes.AddLast(note);
                    notePlayed = note;
                }
            }
            if (notePlayed != null) {
                AudioManager.PlayNote(notePlayed.audio);

                ParticleSystem m_fx = data.noteFX;
                ParticleSystem.TextureSheetAnimationModule m_anim = m_fx.textureSheetAnimation;
                m_anim.rowIndex = notePlayed.FXRowNumber;
                Instantiate(m_fx, new Vector2(data.transform.position.x, data.col.bounds.max.y), Quaternion.Euler(data.noteFX.transform.rotation.eulerAngles));
                m_fx.GetComponent<FXdestroyer>().hasPlayed = true;
            }

            while (mData.PlayedNotes.Count > mData.MaxSavedNotes) {
                mData.PlayedNotes.RemoveFirst();
            }

        // Checks id a melody have been played
            foreach (Melody melody in mData.melodies)
            {
                if (melody.CheckMelody(mData.PlayedNotes))
                {
                    mData.currentMelody = melody.melodyID;

                    if (mData.currentMelody == Melody.MelodyID.MagicResistMelody) {
                        if (PlayedMagicResist != null) {
                            PlayedMagicResist();
                        }
                    }
                    mData.playMelodyState = false;
                    mData.MelodyRange.enabled = true;

                    data.MelodyPlayed(melody.melodyID);

                    AudioManager.FadeBGMBackToNormal();
                    mData.PlayedNotes.Clear();

                    break;
                }
            }
        }         
    }
}
