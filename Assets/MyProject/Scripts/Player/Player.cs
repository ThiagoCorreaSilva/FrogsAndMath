using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class Player : LifeController
{
    private Rigidbody2D rb;
    private Animator anim;

    [Header("Movement")]
    private Vector2 direction;
    [SerializeField] private float speed;
    [SerializeField] private bool facingLeft;
    public bool canMove;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    protected override void Start()
    {
        rb.gravityScale = 0f;

        canMove = true;
    }

    private void Update()
    {
        Inputs();
        Anim();

        if (direction.x < 0 && !facingLeft || direction.x > 0 && facingLeft) Flip();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Inputs()
    {
        if (!canMove) return;

        float _x = Input.GetAxisRaw("Horizontal");
        float _y = Input.GetAxisRaw("Vertical");

        direction = new Vector2(_x, _y);
    }

    private void Move()
    {
        rb.velocity = direction * speed * Time.fixedDeltaTime;
    }

    private void Flip()
    {
        facingLeft = !facingLeft;
        transform.Rotate(Vector2.up * 180f);
    }

    private void Anim()
    {
        anim.SetFloat("Speed_X", direction.x);
        anim.SetFloat("Speed_Y", direction.y);
    }
}
