using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class House : MonoBehaviour
{
    public Transform houseLocation;
    public bool automatic;
    public MapBoundary mb;
    
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
        print("player: " + player.transform.position + "..." + "Location: " + houseLocation.position);
        player.transform.position = houseLocation.position;
        mb.UpdateMapBounds();
        Camera.main.GetComponent<CameraFollow2D>().UpdateToMapBounds();
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
