using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownController : MonoBehaviour
{
    public static Transform Cadenza;
    public static bool controllable;
    public float speed;
    private Vector2 direction;

    private Vector3 startPos;
    private SpriteRenderer sprRend;
    private Rigidbody2D rb;

    void Awake()
    {
        Cadenza = transform;

        sprRend = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        controllable = false;
        startPos = transform.position;

        BattleScene.EnemysTurn += ActivateController;
        BulletPattern.PatternEnded += DeactivateController;
        BattleState.BattleEnded += DeactivateController;
    }

    void OnDestroy()
    {
        BattleScene.EnemysTurn -= ActivateController;
        BulletPattern.PatternEnded -= DeactivateController;
        BattleState.BattleEnded -= DeactivateController;
    }

    private void ActivateController()
    {
        transform.position = startPos;
        controllable = true;
    }

    private void DeactivateController()
    {
        controllable = false;
    }

    void Update()
    {
        sprRend.enabled = controllable;

        if (!controllable)
        {
            return;
        }

        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) + Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0)
        {
            direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        } else
        {
            direction = new Vector2(Input.GetAxisRaw("BattleHorizontal"), Input.GetAxisRaw("BattleVertical"));
        }
        direction.Normalize();
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + direction * speed * Time.deltaTime);
    }
}
