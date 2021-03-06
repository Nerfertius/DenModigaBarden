﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/Player/PlayerCastMelodyProjectile")]
public class PlayerCastMelodyProjectile : StateAction {

    public Vector3 SpawnOffset = new Vector3(1, 0, 0);

    public override void Act(StateController controller) {
        PlayerData data = (PlayerData)controller.data;
        PlayerData.MelodyData mData = data.melodyData;

        bool facingRight = true;

        if (mData.currentMelody != null && Input.GetButtonDown("MelodyProjectileCast") && mData.projectileCooldownTimer.IsDone()) {
            Vector3 offset = SpawnOffset;

            facingRight = data.transform.localScale.x > 0;

            if (!facingRight) {
                offset = Vector3.Scale(SpawnOffset, new Vector3(-1, 0, 0));
            }

            GameObject newProjectile = null;
            switch (mData.currentMelody) {
                case Melody.MelodyID.JumpMelody:
                    newProjectile = Instantiate(mData.JumpMelodyProjectile);
                    break;
                case Melody.MelodyID.SleepMelody:
                    newProjectile = Instantiate(mData.SleepMelodyProjectile);
                    break;
                case Melody.MelodyID.MagicResistMelody:
                    newProjectile = Instantiate(mData.MagicResistMelodyProjectile);
                    break;
            }
            
            MelodyProjectile melodyProjectile = newProjectile.GetComponent<MelodyProjectile>();
            melodyProjectile.Init(controller.transform.position + offset, facingRight);

            mData.projectileCooldownTimer.Start();
        }
    }
}
