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

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update ()
    {
        if ((Input.GetButtonDown("EnterHouse") || Input.GetAxisRaw("EnterHouse") > 0.75f) && playerNear && !automatic)
        {
            StartCoroutine(EnterHouse());
        }
	}

    IEnumerator EnterHouse()
    {
        CameraFX.FadeIn();
        yield return new WaitForSeconds(1f);
        player.transform.position = houseLocation.position;
        mb.CalculateMapBounds();
        mb.UpdateMapBounds();
        Camera.main.GetComponent<CameraFollow2D>().UpdateToMapBounds();
        CameraFX.FadeOut();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerNear = true;
            if (automatic)
            {
                StartCoroutine(EnterHouse());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerNear = false;
        }
    }
}
