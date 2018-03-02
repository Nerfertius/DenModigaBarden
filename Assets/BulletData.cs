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

    private void Update()
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
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
                StartCoroutine(HomingBehaviour());
                break;
            default:
                break;
        }
    }

    IEnumerator HomingBehaviour()
    {
        float travelDistance = 0;
        float maxTravelDistance = 4f;

        for (int i = 0; i < 3; i++)
        {
            while (travelDistance < maxTravelDistance)
            {
                direction = TopDownController.Cadenza.position - transform.position;
                travelDistance += speed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, TopDownController.Cadenza.position, speed * Time.deltaTime);
            }
            yield return new WaitForSeconds(1);
            travelDistance = 0;
        }

        StartCoroutine(FadeOut());
    }

    IEnumerator BiteBehaviour()
    {
        float travelDistance = 0;
        float maxTravelDistance = 4f;

        float velocity;
        while(travelDistance < maxTravelDistance)
        {
            speed += (speed + 1f) * Time.deltaTime;
            velocity = direction.y * speed * Time.deltaTime;
            transform.position += new Vector3(0, velocity, 0);
            travelDistance += Mathf.Abs(velocity);
            yield return null;
        }

        while (speed > 0)
        {
            speed -= (speed + 10f) * Time.deltaTime;
            transform.position += new Vector3(0, direction.y * speed * Time.deltaTime, 0);
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
            StopAllCoroutines();
            this.gameObject.SetActive(false);
        }
    }
}
