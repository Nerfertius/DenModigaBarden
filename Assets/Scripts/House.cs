using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class House : MonoBehaviour
{
    public Transform houseLocation;
    public bool automatic;
    
    private GameObject player;
    private bool playerNear;
    
    void Update ()
    {
        if (Input.GetButtonDown("EnterHouse") && playerNear && !automatic)
        {
            StartCoroutine(EnterHouse());
        }
        if (playerNear && automatic)
        {
            StartCoroutine(EnterHouse());
        }
	}

    IEnumerator EnterHouse()
    {
        CameraFX.FadeIn();
        yield return new WaitForSeconds(1f);
        player.transform.position = houseLocation.position;
        CameraFX.FadeOut();
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
