using System.Collections;
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

    public enum PatternType { GoblinBite, EvilEyeHoming }
    public PatternType pattern;

    private int activeCoroutines = 0;

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
                spawner.StartCoroutine(GoblinBitePattern(1, 6, BattleScene.instance.topLeft.position, new Vector2(0, -1)));
                spawner.StartCoroutine(GoblinBitePattern(1, 6, BattleScene.instance.bottomLeft.position + new Vector3(0.5f, 0, 0), new Vector2(0, 1)));                
                break;
            case PatternType.EvilEyeHoming:
                break;
            default:
                break;
        }
    }

    private void PatternEnding()
    {
        activeCoroutines--;

        if(activeCoroutines == 0 && PatternEnded != null)
        {
            PatternEnded.Invoke();
        }
    }

    IEnumerator GoblinBitePattern(float times, float xAmount, Vector2 startPos, Vector2 direction)
    {
        activeCoroutines++;
        float xPos = startPos.x;
        float yPos = startPos.y;

        for (int x = 0; x < times; x++)
        {
            for (int y = 0; y < xAmount; y++)
            {
                for (int z = 0; z < bullets.Count; z++)
                {
                    if (!bullets[z].activeSelf)
                    {
                        bullets[z].SetActive(true);
                        bullets[z].transform.position = new Vector3(xPos, yPos, 0);
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

            xPos = startPos.x;
            yield return new WaitForSecondsRealtime(1.5f);
        }

        PatternEnding();
    }
}
