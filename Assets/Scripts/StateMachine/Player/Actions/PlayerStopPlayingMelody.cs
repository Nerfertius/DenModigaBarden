using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/Player/PlayerStopPlayingMelody")]
public class PlayerStopPlayingMelody : StateAction {
   public override void ActOnce(StateController controller) {
       PlayerData data = (PlayerData)controller.data;
       
   }
}
