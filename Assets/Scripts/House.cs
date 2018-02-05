using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class House : MonoBehaviour
{
    public string houseScene;
    
    private GameObject player;
    private bool playerNear;
    
    void Update ()
    {
        if (Input.GetButtonDown("EnterHouse") && playerNear)
        {
            StartCoroutine(EnterHouse());
        }
	}

    IEnumerator EnterHouse()
    {
        CameraFX.FadeIn();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(houseScene);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerNear = true;
            player = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerNear = false;
            player = null;
        }
    }
}
