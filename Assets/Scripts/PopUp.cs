﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    private bool hasShowed;
    private bool hasCleared;
    private bool visible;
    private SpriteRenderer rend;
    private StateController npcState;
    
    public Sprite sprite;
    public bool repeatable;
    public bool showUntilCleared;
    public Vector2 offset;

    [Header("NPC")]
    public bool npcTalk;
    public State state;
    [Header("Melody")]
    public bool melodyPlayed;
    public Melody.MelodyID melody;
    [Header("Action")]
    public bool actionPerformed;
    public string buttonName;


    private void OnDrawGizmosSelected()
    {
        Vector2 pos = (Vector2)transform.position + offset;
        Gizmos.DrawCube(pos, Vector2.one / 2);
    }

    void Start ()
    {
        if (npcTalk)
        {
            npcState = transform.parent.GetComponent<StateController>();
        }

        GameObject empty = new GameObject();
        empty.AddComponent<SpriteRenderer>();
        GameObject prompt = Instantiate(empty, (Vector2)transform.position + offset, Quaternion.identity);
        Destroy(empty);
        prompt.transform.parent = transform;
        rend = prompt.GetComponent<SpriteRenderer>();
        rend.sprite = sprite;
        Color temp = rend.color;
        temp.a = 0;
        rend.color = temp;
        rend.sortingLayerName = "UI";

        if (!npcTalk && !melodyPlayed && !actionPerformed)
        {
            Debug.Log("Error, PopUp needs at least 1 condition");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !visible)
        {
            if (repeatable)
            {
                StopAllCoroutines();
                StartCoroutine("FadeIn");
            }

            else if (!repeatable && !hasShowed && !visible)
            {
                StopAllCoroutines();
                StartCoroutine("FadeIn");
                hasShowed = true;
            }

            else if (!repeatable && showUntilCleared && !hasCleared && !visible)
            {
                StopAllCoroutines();
                StartCoroutine("FadeIn");
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerData data = collision.GetComponent<PlayerData>();
            PlayerData.MelodyData mData = data.melodyData;

            if (npcTalk)
            {
                if (npcState.currentState == state)
                {
                    StopAllCoroutines();
                    StartCoroutine("FadeOut");
                    if (showUntilCleared)
                    {
                        hasCleared = true;
                    }
                }
                else if (npcState.currentState.ToString() == "NPCIdle (State)")
                {
                    StopAllCoroutines();
                    StartCoroutine("FadeIn");
                }
            }

            else if (melodyPlayed && mData.currentMelody == melody)
            {
                StopAllCoroutines();
                StartCoroutine("FadeOut");
                if (showUntilCleared)
                {
                    hasCleared = true;
                }
            }

            else if (actionPerformed && Input.GetButtonDown(buttonName))
            {
                StopAllCoroutines();
                StartCoroutine("FadeOut");
                if (showUntilCleared)
                {
                    hasCleared = true;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && visible)
        {
            StopAllCoroutines();
            StartCoroutine("FadeOut");
        }
    }

    IEnumerator FadeIn()
    {
        visible = true;
        Color newColor = rend.color;
        while (rend.color.a < 1)
        {
            newColor.a += 0.01f;
            rend.color = newColor;
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator FadeOut()
    {
        visible = false;
        Color newColor = rend.color;
        while (rend.color.a > 0)
        {
            newColor.a -= 0.01f;
            rend.color = newColor;
            yield return new WaitForEndOfFrame();
        }
    }
}