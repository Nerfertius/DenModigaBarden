using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletData : Data {
    public float startSpeed;
    private float speed;

    [HideInInspector] public Vector2 direction;
    [HideInInspector] public BulletPattern.PatternType bulletType;

    private SpriteRenderer sprRend;
    private CapsuleCollider2D coll;
    private Color startColor;

    private void Awake()
    {
        sprRend = GetComponent<SpriteRenderer>();
        coll = GetComponent<CapsuleCollider2D>();

        startColor = sprRend.color;
    }

    private void OnEnable()
    {
        speed = startSpeed;
        sprRend.color = startColor;
        
        switch (bulletType)
        {
            case BulletPattern.PatternType.GoblinBite:
                StartCoroutine(BiteBehaviour());
                break;
            case BulletPattern.PatternType.EvilEyeHoming:
                break;
            default:
                break;
        }
    }

    IEnumerator BiteBehaviour()
    {
        float travelDistance = 0;
        float maxTravelDistance = 4f;

        while(travelDistance < maxTravelDistance)
        {
            speed += (speed + 0.5f) * Time.deltaTime;
            transform.position += new Vector3(0, direction.y * speed * Time.deltaTime, transform.position.z);
            travelDistance += Mathf.Abs(direction.y * speed * Time.deltaTime);
            yield return null;
        }

        while (speed > 0)
        {
            speed -= (speed + 10f) * Time.deltaTime;
            transform.position += new Vector3(0, direction.y * speed * Time.deltaTime, transform.position.z);
            yield return null;
        }

        StartCoroutine(FadeOut());
    }


    IEnumerator FadeOut()
    {
        Color c = sprRend.color;

        for (float value = sprRend.color.a; value >= 0; value -= 0.1f)
        {
            c.a = Mathf.Clamp01(value);
            sprRend.color = c;

            yield return new WaitForSeconds(0.01f);
        }

        this.gameObject.SetActive(false);
    }

    private void OnBecameInvisible()
    {
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerData.player.health -= 0.5f;
            this.gameObject.SetActive(false);
        }
    }
}
