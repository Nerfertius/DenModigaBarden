﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletPattern
{
    private BulletHellSpawner spawner;
    private List<GameObject> bullets;
    private List<BulletData> datas;

    public delegate void PatternEndedEventHandler();
    public static event PatternEndedEventHandler PatternEnded;

    public enum PatternType { GoblinBite, EvilEyeHoming, GargoyleStomp }
    public PatternType pattern;

    private static int activeCoroutines = 0;

    public void SetReferences(BulletHellSpawner spawner, List<GameObject> bullets, List<BulletData> datas)
    {
        this.spawner = spawner;
        this.bullets = bullets;
        this.datas = datas;
    }

    public void PlayPattern()
    {
        switch (pattern)
        {
            case PatternType.GoblinBite:
                spawner.StartCoroutine(GoblinBitePattern(3, 4, BattleScene.instance.topLeft.position, new Vector2(0, 3), new Vector2(0, -1)));
                spawner.StartCoroutine(GoblinBitePattern(3, 3, BattleScene.instance.bottomLeft.position, new Vector2(0.5f, -3f), new Vector2(0, 1)));                
                break;
            case PatternType.EvilEyeHoming:
                break;
            case PatternType.GargoyleStomp:
                break;
            default:
                break;
        }
    }

    public void ResetCoroutineCount()
    {
        activeCoroutines = 0;
    }

    private void PatternEnding()
    {
        activeCoroutines--;

        if(activeCoroutines == 0 && PatternEnded != null)
        {
            PatternEnded.Invoke();
        }
    }
    
    IEnumerator HomingPattern()
    {
        activeCoroutines++;

        //TODO

        PatternEnding();
        yield return null;
    }

    IEnumerator GargoyleStompPattern()
    {
        activeCoroutines++;

        //TODO

        PatternEnding();
        yield return null;
    }

    IEnumerator GoblinBitePattern(float times, float xAmount, Vector2 startPos, Vector2 offset, Vector2 direction)
    {
        activeCoroutines++;
        float xPos = TopDownController.Cadenza.position.x - 1.5f + offset.x;
        float yPos = TopDownController.Cadenza.position.y + offset.y;
        
        for (int x = 0; x < times; x++)
        {
            xPos = (TopDownController.Cadenza.position.x - 1.5f) + offset.x;

            for (int y = 0; y < xAmount; y++)
            {
                for (int z = 0; z < bullets.Count; z++)
                {
                    if (!bullets[z].activeSelf)
                    {
                        bullets[z].SetActive(true);
                        bullets[z].transform.position = new Vector3(xPos, yPos, 0);
                        bullets[z].transform.localScale = new Vector3(1, 1, 1);

                        if (y == 0 || y == xAmount - 1)
                        {
                            bullets[z].transform.localScale = new Vector3(1, 2, 1);
                        }

                        datas[z].bulletType = pattern;
                        datas[z].direction = direction;
                        xPos += 1;
                        break;
                    }
                }

                if (y >= xAmount)
                {
                    break;
                }
            }

            yield return new WaitForSeconds(1.5f);
        }

        yield return new WaitForSeconds(1f);
        PatternEnding();
    }
}
