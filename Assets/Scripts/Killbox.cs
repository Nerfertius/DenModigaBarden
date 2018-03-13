using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killbox : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerData.player.body.velocity = Vector2.zero;
            StartCoroutine(PlayerData.player.Respawn());
        }
    }
}
