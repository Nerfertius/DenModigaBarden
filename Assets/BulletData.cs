using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletData : Data {
    public float startSpeed;
    public bool destroyable;
    public bool childProjectile;
    public RuntimeAnimatorController childController;
    public AudioClip soundEffect;

    private float speed;
    

    [HideInInspector] public Vector2 direction;
	[HideInInspector] public BulletPattern.PatternType bulletType;
    [HideInInspector] private Animator anim;

    private List<GameObject> bullets;
	private List<BulletData> datas;
	private RuntimeAnimatorController startController;
    private SpriteRenderer sprRend;
	private Color startColor;
    private Transform startParent;
    private Quaternion startRotation;

    private void Awake ()
	{
		sprRend = GetComponent<SpriteRenderer> ();
		anim = GetComponent<Animator> ();

		if (anim != null) {
        	startController = anim.runtimeAnimatorController;
		}
		startParent = transform.parent;
        startColor = sprRend.color;
        startRotation = transform.rotation;
    }

    private void Update ()
	{
		float angle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg + 90;
		transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);

		if (childProjectile) {
			Vector3 velocity = new Vector3(direction.x * speed, direction.y * speed, 0);
			transform.position += velocity * Time.deltaTime;
		}
    }

    private void OnEnable ()
	{
		if (!childProjectile) {
			switch (bulletType) {
			case BulletPattern.PatternType.GoblinBite:
				StartCoroutine (BiteBehaviour ());
				break;
			case BulletPattern.PatternType.EvilEyeSpiral:
				StartCoroutine (CirclingSpiralBehaviour ());
				break;
            case BulletPattern.PatternType.GargoyleSmash:
                StartCoroutine(SmashBehaviour());
                break;
            case BulletPattern.PatternType.KoboldSpearAttack:
                StartCoroutine(LockOnSpearBehaviour());
                break;
            case BulletPattern.PatternType.EvilEyeCenter:
                StartCoroutine(CirclingCenterBehaviour());
                break;
			default:
				break;
			}
		}
	}

	private void OnDisable ()
	{
		if (anim != null) {
			anim.runtimeAnimatorController = startController;
		}
		speed = startSpeed;
		sprRend.color = startColor;
        transform.rotation = startRotation;
        childProjectile = false;
	}

    public void SetReferences(List<GameObject> bullets, List<BulletData> datas)
    {
        this.bullets = bullets;
        this.datas = datas;
    }

    IEnumerator SmashBehaviour()
    {
        float travelDistance = 0;
        float maxTravelDistance = 1f;

        while(travelDistance < maxTravelDistance) {
            travelDistance += Mathf.Abs(direction.x * speed * Time.deltaTime);
            transform.position += new Vector3(direction.x * speed * Time.deltaTime, 0, 0);
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);
        speed = 30f;

		Vector3 targetPos = new Vector3(-direction.x * sprRend.sprite.bounds.size.y * 0.5f, transform.position.y, transform.position.z);

        while (transform.position != targetPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        AudioManager.PlayOneShot(soundEffect);
        BattleBox.ShakeBox(0.5f, 0.10f, 0.10f);

        StartCoroutine(FadeOut());
    }

    IEnumerator CirclingSpiralBehaviour ()
	{
        yield return new WaitForSeconds(0.5f);
		for (int n = 0; n < 42; n++) {
			for (int m = 0; m < bullets.Count; m++) {
				if (!bullets[m].activeSelf) {
					datas[m].destroyable = true;
					datas[m].childProjectile = true;
					datas[m].direction = (Vector2)(Quaternion.Euler(0,0,n * 45) * Vector2.right);
					datas[m].anim.runtimeAnimatorController = childController;
					bullets[m].SetActive(true);
					bullets[m].transform.localPosition = transform.position;
					datas[m].speed = 2f;
                    yield return new WaitForSeconds(0.15f);
                    break;
				}
			}
		}

        yield return new WaitForSeconds(2);
        StartCoroutine(FadeOut());
    }
    IEnumerator CirclingCenterBehaviour()
    {
        yield return new WaitForSeconds(0.5f);
        for (int n = 0; n < 35; n++)
        {
            for (int m = 0; m < bullets.Count; m++)
            {
                if (!bullets[m].activeSelf)
                {
                    datas[m].destroyable = true;
                    datas[m].childProjectile = true;
                    datas[m].direction = Vector3.Normalize(BattleScene.instance.center.position - transform.position);
                    datas[m].anim.runtimeAnimatorController = childController;
                    bullets[m].SetActive(true);
                    bullets[m].transform.localPosition = transform.position;
                    datas[m].speed = 2.5f;
                    yield return new WaitForSeconds(0.25f);
                    break;
                }
            }
        }

        yield return new WaitForSeconds(2);
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

    IEnumerator LockOnSpearBehaviour()
    {
        float timer = 0;
        float lockOnTime = 1f;
        float travelDistance = 0;
        float maxTravelDistance = 2f;

        while (timer < lockOnTime)
        {
            direction = TopDownController.Cadenza.position - transform.position;
            direction.Normalize();
            timer += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(0.2f);
        while (timer >= lockOnTime && travelDistance < maxTravelDistance)
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
            travelDistance += Time.deltaTime;
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

        transform.parent = null;
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

            if (destroyable) { 
                StopAllCoroutines();
                this.gameObject.SetActive(false);
            }
        }
    }
}
