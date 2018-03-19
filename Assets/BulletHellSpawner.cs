using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHellSpawner : MonoBehaviour {

    public BulletPattern[] patterns;
    private int patternIndex = 0;

	void Start () {
        for (int i = 0; i < patterns.Length; i++)
        {
            patterns[i].SetReference(this);
        }
    }

    private void OnEnable()
    {
        BattleScene.EnemysTurn += StartNextAttack;
        BattleState.BattleEnded += DisableAllBullets;
        BulletPattern.PatternEnded += DisableAllBullets;

        patternIndex = 0;

        for (int i = 0; i < patterns.Length; i++)
        {
            patterns[i].InstantiateBullets();
        }
    }

    private void OnDisable()
    {
        BattleScene.EnemysTurn -= StartNextAttack;
		BattleState.BattleEnded -= DisableAllBullets;
        BulletPattern.PatternEnded -= DisableAllBullets;

        patternIndex = 0;

        for (int i = 0; i < patterns.Length; i++)
        {
            patterns[i].DestroyBullets();
        }
    }

    private void DisableAllBullets()
    {
        StopAllCoroutines();

        foreach (BulletPattern pattern in patterns)
        {
            pattern.ResetCoroutineCount();
            pattern.DisableBullets();
        }
    }

    private void StartNextAttack()
    {
        StartCoroutine(DelayPattern());
    }

    IEnumerator DelayPattern()
    {
        yield return new WaitForSeconds(0.5f);
        patterns[patternIndex].PlayPattern();
        patternIndex++;
        if(patternIndex >= patterns.Length)
        {
            patternIndex = 0;
        }
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
