using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBullet : MonoBehaviour {

    [HideInInspector] public Transform target;
    public float speed;
    public float followDuration;

    private float timer;
    private float timeMultiplier = 1f;
    private Rigidbody2D rb;
    
    private Vector3 lastDirection = Vector3.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate () {
        timer += Time.deltaTime;

        if(target != null)
        {
            Vector3 direction = target.position - transform.position;
            direction.Normalize();

            if (timer <= followDuration)
            {
                lastDirection = direction;
            }
        }

        rb.AddForce(lastDirection * speed * timeMultiplier);

        timeMultiplier += Time.deltaTime;
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
