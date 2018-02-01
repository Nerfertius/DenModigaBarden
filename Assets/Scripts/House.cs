using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    public Transform houseLocation;
    
    private GameObject player;
    private bool playerNear;
    
    void Update ()
    {
        if (Input.GetButtonDown("EnterHouse") && playerNear)
        {
            //StartCoroutine(EnterHouse());
        }
	}

    //IEnumerator EnterHouse()
    //{
    //    // FadeIn & FadeOut doesn't work

    //    //CameraFX.FadeOut();
    //    //player.transform.position = houseLocation.position;
    //    //yield return new WaitForSeconds(2f);
    //    //CameraFX.FadeIn();
    //}

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
