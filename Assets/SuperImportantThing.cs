using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperImportantThing : MonoBehaviour
{
    private bool running;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (running != true)
        {
            StartCoroutine(Play());
            running = true;
        }
    }

    IEnumerator Play()
    {
        GetComponent<Animator>().SetTrigger("Play");
        yield return new WaitForSeconds(20);
        running = false;
    }
}
