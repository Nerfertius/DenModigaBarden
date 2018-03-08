using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletPattern
{
    private BulletHellSpawner spawner;

    public GameObject bulletPrefab;
    public int poolSize;

    private List<GameObject> bullets = new List<GameObject>();
    private List<BulletData> datas = new List<BulletData>();

    public delegate void PatternEndedEventHandler();
    public static event PatternEndedEventHandler PatternEnded;

    public enum PatternType { GoblinBite, EvilEyeSpiral, GargoyleStomp, KoboldSpearAttack, EvilEyeCenter}
    public PatternType pattern;

    private static int activeCoroutines = 0;

    public void SetReference(BulletHellSpawner spawner)
    {
        this.spawner = spawner;
    }

    public void InstantiateBullets()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = GameObject.Instantiate(bulletPrefab);
            BulletData data = bullet.GetComponent<BulletData>();

            if (data != null)
            {
                datas.Add(data);
            }
            else
            {
                Debug.LogWarning("Missing bullet data");
            }

            bullet.SetActive(false);
            bullets.Add(bullet);
        }
    }

    public void DestroyBullets()
    {
        foreach (GameObject bullet in bullets)
        {
            GameObject.Destroy(bullet);
        }

        bullets.Clear();
        datas.Clear();
    }

    public void DisableBullets()
    {
        foreach (GameObject bullet in bullets)
        {
            bullet.SetActive(false);
            bullet.transform.parent = null;
        }
    }

    public void PlayPattern()
    {
        switch (pattern)
        {
            case PatternType.GoblinBite:
                spawner.StartCoroutine(GoblinBitePattern(3, 4, new Vector2(0, 3), new Vector2(0, -1)));
                spawner.StartCoroutine(GoblinBitePattern(3, 3, new Vector2(0.5f, -3f), new Vector2(0, 1)));                
                break;
            case PatternType.EvilEyeSpiral:
				spawner.StartCoroutine(CirclingSpiralPattern(BattleScene.instance.center.position));
                break;
            case PatternType.GargoyleStomp:
                spawner.StartCoroutine(GargoyleStompPattern(5));
                break;
            case PatternType.KoboldSpearAttack:
                spawner.StartCoroutine(KoboldSpearAttack(10));
                break;
            case PatternType.EvilEyeCenter:
                spawner.StartCoroutine(CirclingCenterPattern(BattleScene.instance.center.position));
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
    
    IEnumerator CirclingSpiralPattern(Vector3 spawnPosition)
    {
        activeCoroutines++;

        int spawned = 0;

        for (int z = 0; z < bullets.Count; z++)
        {
            if (!bullets[z].activeSelf)
            {
                spawned++;

                datas[z].bulletType = pattern;
                datas[z].SetReferences(bullets, datas);
                bullets[z].SetActive(true);
                float offset = 2.5f;
				switch (spawned) {
					case 1:
						bullets[z].transform.position = spawnPosition + new Vector3(-offset, 0, 0);
						break;
					case 2:
						bullets[z].transform.position = spawnPosition + new Vector3(offset, 0, 0);
						break;
					default:
						break;
				}
                bullets[z].transform.localScale = new Vector3(1, 1, 1);
                bullets[z].transform.parent = BattleScene.instance.center;
                
                if (spawned >= 2)
                {
                    break;
                }
            }
        }

        yield return new WaitForSeconds(10f);
        PatternEnding();
    }

    IEnumerator CirclingCenterPattern(Vector3 spawnPosition)
    {
        activeCoroutines++;

        int spawned = 0;

        for (int z = 0; z < bullets.Count; z++)
        {
            if (!bullets[z].activeSelf)
            {
                spawned++;

                datas[z].bulletType = pattern;
                datas[z].SetReferences(bullets, datas);
                bullets[z].SetActive(true);
                float offset = 2.5f;
                switch (spawned)
                {
                    case 1:
                        bullets[z].transform.position = spawnPosition + new Vector3(-offset, 0, 0);
                        break;
                    case 2:
                        bullets[z].transform.position = spawnPosition + new Vector3(offset, 0, 0);
                        break;
                    case 3:
                        bullets[z].transform.position = spawnPosition + new Vector3(0, -offset, 0);
                        break;
                    case 4:
                        bullets[z].transform.position = spawnPosition + new Vector3(0, offset, 0);
                        break;
                    default:
                        break;
                }
                bullets[z].transform.localScale = new Vector3(1, 1, 1);
                bullets[z].transform.parent = BattleScene.instance.center;

                if (spawned >= 4)
                {
                    break;
                }
            }
        }

        yield return new WaitForSeconds(12.5f);
        PatternEnding();
    }

    IEnumerator GargoyleStompPattern(int times)
    {
        activeCoroutines++;

        int spawned = 0;

        for (int i = 0; i < times; i++)
        {
            float xOffset = 10.5f;
            float yOffset = TopDownController.Cadenza.localPosition.y;
            for (int n = 0; n < bullets.Count; n++)
            {
                if (!bullets[n].activeSelf)
                {
                    spawned++;

                    datas[n].bulletType = pattern;
                    
                    switch (spawned)
                    {
                        case 1:
                            bullets[n].transform.position = BattleScene.instance.center.position + new Vector3(-xOffset, yOffset);
                            datas[n].direction = new Vector2(1, 0);
                            bullets[n].transform.Rotate(0, 0, 90);
                            bullets[n].transform.localScale = new Vector3(-1, 1, 1);
                            break;
                        case 2:
                            bullets[n].transform.position = BattleScene.instance.center.position + new Vector3(xOffset, yOffset);
                            datas[n].direction = new Vector2(-1, 0);
                            bullets[n].transform.Rotate(0, 0, -90);
                            break;
                        default:
                            break;
                    }

                    bullets[n].SetActive(true);

                    if (spawned >= 2)
                    {
                        spawned = 0;
                        break;
                    }
                }
            }
            yield return new WaitForSeconds(1.5f);
        }

        yield return new WaitForSeconds(1);
        PatternEnding();
    }

    IEnumerator KoboldSpearAttack(int times)
    {
        activeCoroutines++;
        bool axisToggle = false;

        for (int i = 0; i < times; i++)
        {
            for (int n = 0; n < bullets.Count; n++)
            {
                if (!bullets[n].activeSelf)
                {
                    float xPos = 0;
                    float yPos = 0;
                    SpearRandomSpawn(out xPos, out yPos, axisToggle);
                    axisToggle = !axisToggle;

                    datas[n].bulletType = pattern;
                    bullets[n].transform.position = BattleScene.instance.center.position + new Vector3(xPos, yPos, 0f);
                    bullets[n].SetActive(true);
                    break;
                }
                yield return new WaitForSeconds(0.2f);
            }
        }

        yield return new WaitForSeconds(4f);
        PatternEnding();
    }

    IEnumerator GoblinBitePattern(float times, float xAmount, Vector2 offset, Vector2 direction)
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
                        datas[z].bulletType = pattern;
                        datas[z].direction = direction;
                        bullets[z].SetActive(true);
                        bullets[z].transform.position = new Vector3(xPos, yPos, 0);
                        bullets[z].transform.localScale = new Vector3(1, 1, 1);

                        if (y == 0 || y == xAmount - 1)
                        {
                            bullets[z].transform.localScale = new Vector3(1, 2, 1);
                        }

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

    void SpearRandomSpawn(out float xValue, out float yValue, bool axisToggle)
    {
        bool xRand = Random.value > 0.5f;
        bool yRand = Random.value > 0.5f;
        Bounds playArea = BattleScene.instance.playAreaBounds;

        xValue = 0;
        yValue = 0;

        if (axisToggle)
        {

            xValue = Random.Range((-playArea.size.x * 0.5f) - 2f, (playArea.size.x * 0.5f) + 2f);

            if (yRand)
            {
                yValue = Random.Range(playArea.size.y * 0.5f + 1f, (playArea.size.y * 0.5f) + 3f);
            }
            else
            {
                yValue = Random.Range(-playArea.size.y * 0.5f - 1f, (-playArea.size.y * 0.5f) - 3f);
            }
        }
        else if (!axisToggle)
        {
            yValue = Random.Range((-playArea.size.y * 0.5f) - 3f, (playArea.size.y * 0.5f) + 3f);

            if (xRand)
            {
                xValue = Random.Range(playArea.size.x * 0.5f, (playArea.size.x * 0.5f) + 2f);
            }
            else
            {
                xValue = Random.Range(-playArea.size.x * 0.5f, (-playArea.size.x * 0.5f) - 2f);
            }
            
        }
        

        //Debug.Log("xValue: " + xValue + "     " + "YValue: " + yValue);
        
        if ((xValue > playArea.min.x && xValue < playArea.max.x) || (yValue > playArea.min.y && yValue < playArea.max.y))
        {
            Debug.Log("Wiiiiiie");
        }
    }
}
