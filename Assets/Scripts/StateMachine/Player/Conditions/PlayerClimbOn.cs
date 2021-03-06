﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Condition/Player/PlayerClimbOn")]
public class PlayerClimbOn : Condition
{
    public override bool? CheckTriggerEnter(StateController controller, Collider2D other)
	{
        if (other.CompareTag("Ladder"))
        {
            PlayerData data = (PlayerData)controller.data;
            data.ladderBottom = other.transform.GetComponentInParent<LadderBuilder>().bottomLadder;
            data.ladderTop = other.transform.GetComponentInParent<LadderBuilder>().topLadder;
        }
        return null;
	}

    public override bool? CheckTriggerExit(StateController controller, Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            PlayerData data = (PlayerData)controller.data;
            data.ladderBottom = null;
            data.ladderTop = null;
        }
        return null;
    }

    public override bool? CheckCondition(StateController controller)
    {
        PlayerData data = (PlayerData)controller.data;

        if (data.ladderBottom == null && data.ladderTop == null)
        {
            return null;
        }
        else
        {
            if (!data.climbPause)
            {
                Collider2D botCol = data.ladderBottom.GetComponent<Collider2D>();
                Collider2D topCol = data.ladderTop.GetComponent<Collider2D>();
                float feet = data.col.bounds.min.y;

                if (data.col.bounds.center.x > botCol.bounds.center.x - 0.3f
                   && data.col.bounds.center.x < botCol.bounds.center.x + 0.3f
                   && feet < topCol.bounds.max.y + 0.5f)
                {
                    //Bottom
                    if (Input.GetAxisRaw("Vertical") > data.axisSensitivity && feet > botCol.bounds.min.y && data.col.bounds.center.y < topCol.bounds.min.y)
                    {
                        return true;
                    }

                    //Top
                    else if (Input.GetAxisRaw("Vertical") < -data.axisSensitivity && feet < topCol.bounds.max.y + 0.2f && feet > botCol.bounds.max.y)
                    {
                        return true;
                    }
                }
                return null;
            }
            else if (data.climbPause)
            {
                return null;
            }
            return null;
        }
    }
}
