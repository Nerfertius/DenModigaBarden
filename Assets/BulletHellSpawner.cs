using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHellSpawner : MonoBehaviour {
    
    public GameObject bulletPrefab;
    public int poolSize;

    public BulletPattern[] patterns;

    private List<GameObject> bullets;
    private List<BulletData> datas;

    void Awake()
    {
        bullets = new List<GameObject>();
        datas = new List<BulletData>();
    }

	void Start () {
        for (int i = 0; i < patterns.Length; i++)
        {
            patterns[i].SetReferences(this, bullets, datas);
        }
    }

    private void OnEnable()
    {
        BattleScene.EnemysTurn += StartNextAttack;
        BattleState.BattleEnded += DisableAllBullets;
        BulletPattern.PatternEnded += DisableAllBullets;

        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
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

    private void OnDisable()
    {
        BattleScene.EnemysTurn -= StartNextAttack;
		BattleState.BattleEnded -= DisableAllBullets;
        BulletPattern.PatternEnded -= DisableAllBullets;

        foreach (GameObject bullet in bullets)
        {
            Destroy(bullet);
        }

        bullets.Clear();
        datas.Clear();
    }

    private void DisableAllBullets()
    {
        StopAllCoroutines();

        foreach (BulletPattern pattern in patterns)
        {
            pattern.ResetCoroutineCount();
        }

        foreach (GameObject bullet in bullets)
        {
            bullet.SetActive(false);
            bullet.transform.parent = null;
        }
    }

    private void StartNextAttack()
    {
        StartCoroutine(DelayPattern());
    }

    IEnumerator DelayPattern()
    {
        yield return new WaitForSeconds(0.5f);
        patterns[0].PlayPattern();
    }

    /*
    IEnumerator RandomX(float totalBullets)
    {
        float randomX = 0, lastX = 0;

        for(int i = 0; i < totalBullets; i++)
        {
            foreach (GameObject bullet in bullets)
            {
                if (!bullet.activeSelf)
                {
                    bullet.SetActive(true);
                    while (randomX == lastX)
                    {
                        randomX = Random.Range(-2.5f, 2.5f);
                    }
                    lastX = randomX;

                    Vector3 randomOffset = new Vector3(randomX, 0, transform.position.z);
                    bullet.transform.position = transform.position + randomOffset;
                    yield return new WaitForSecondsRealtime(0.05f);
                    break;
                }
            }
        }
    }*/
}
