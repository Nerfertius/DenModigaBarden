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

        BattleScene.instance.SetEnemy(enemyIndex);
        BattleScene.instance.escapeChance = escapeChance;
        BattleScene.instance.enemyHP = enemyHP;

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
