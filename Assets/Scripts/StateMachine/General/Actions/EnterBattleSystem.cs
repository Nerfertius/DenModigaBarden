using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Action/Enemy/EnterBattleSystem")]
public class EnterBattleSystem : StateAction
{
    public int enemyIndex;
    public Sprite battleBackground;
    public AudioClip battleMusic;
    public int escapeChance;
    public int enemyHP;

    public override void ActOnce(StateController controller)
    {
        EnemyData eData = (EnemyData)controller.data;

        if (eData.battleTopBackground != null)
        {
            BattleScene.instance.SetTopBackground(eData.battleTopBackground);
        }
        else
        {
            BattleScene.instance.SetTopBackground(null);
        }

        BattleScene.caller = controller.gameObject;
        BattleScene.instance.SetEnemy(enemyIndex);
        BattleScene.instance.SetEnemyHP(enemyHP);
        BattleScene.instance.escapeChance = escapeChance;

        if(battleBackground != null)
        {
            BattleScene.instance.SetBackground(battleBackground);
        }
        if(battleMusic != null)
        {
            BattleScene.instance.SetBattleMusic(battleMusic);
        }

        if (eData != null)
        {
            GameManager.instance.switchState(new BattleState(GameManager.instance));
        }
    }

}
