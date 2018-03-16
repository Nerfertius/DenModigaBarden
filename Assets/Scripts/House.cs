using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class House : MonoBehaviour
{
    public Transform houseLocation;
    public bool automatic;
    public MapBoundary mb;
    public bool elevator;
    public bool locked;


    public AudioClip audioOnUse;

    private GameObject player;
    private bool playerNear;

    private static int spawnDirection = -1;

    void Start()
    {
        player = PlayerData.player.gameObject;
    }

    void Update ()
    {
        if (Input.GetButtonDown("Interact") && playerNear && !automatic)
        {
            if (!locked)
            {
                StartCoroutine(EnterHouse());
            }
            else if (locked && player.GetComponent<PlayerData>().hasKey)
            {
                StartCoroutine(EnterHouse());
            }
        }
	}

    IEnumerator EnterHouse()
    {
        if (elevator)
        {
            Animator anim = transform.parent.GetComponent<Animator>();
            anim.SetTrigger("Open");
            StartCoroutine(ElevatorFadePlayer(2));
            yield return new WaitForSeconds(1f);
        }
        CameraFX.FadeIn();
        AudioManager.PlayOneShot(audioOnUse);
        yield return new WaitForSeconds(1f);
        player.transform.position = houseLocation.position;

        if (elevator)
        {
            StopAllCoroutines();
            player.GetComponent<SpriteRenderer>().color = Color.white;
        }

        if (GetComponent<SpriteRenderer>() != null)        // Flip the player sprite if they are entering a house
        {
            player.transform.localScale = new Vector3(player.transform.localScale.x * spawnDirection, player.transform.localScale.y, player.transform.localScale.z);
        }
        mb.CalculateMapBounds();
        mb.UpdateMapBounds();
        CameraFollow2D camScript = Camera.main.GetComponent<CameraFollow2D>();
        camScript.enabled = true;
        camScript.UpdateToMapBounds();
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

    IEnumerator ElevatorFadePlayer(float speed)
    {
        SpriteRenderer rend = player.GetComponent<SpriteRenderer>();
        Color newColor = rend.color;
        while (rend.color != Color.black * 0.80f)
        {
            newColor.b = Mathf.Lerp(newColor.b, 0, Time.deltaTime * speed);
            newColor.g = Mathf.Lerp(newColor.g, 0, Time.deltaTime * speed);
            newColor.r = Mathf.Lerp(newColor.r, 0, Time.deltaTime * speed);
            rend.color = newColor;
            yield return new WaitForEndOfFrame();
        }
    }
}
