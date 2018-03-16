using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleBox : MonoBehaviour {

    private static BattleBox instance;
    private Vector3 startPos;

    private void Awake()
    {
        instance = this;
        startPos = transform.position;
    }

    public static void ShakeBox(float duration, float xIntensity, float yIntensity)
    {
        instance.StartCoroutine(instance.ShakeBoxFX(duration, xIntensity, yIntensity));
    }

    IEnumerator ShakeBoxFX(float duration, float xIntensity, float yIntensity)
    {
        float posX = transform.position.x;
        float posY = transform.position.y;

        float timer = 0;

        while (timer < duration)
        {
            timer += 0.05f;

            transform.position = new Vector3(Random.Range(posX - xIntensity, posX + xIntensity), Random.Range(posY - yIntensity, posY + yIntensity), transform.position.z);

            yield return new WaitForSecondsRealtime(0.05f);
        }

        transform.position = startPos;
    }
}
