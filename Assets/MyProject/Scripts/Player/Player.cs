using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    private Vector2 direction;
    [SerializeField] private float speed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        rb.gravityScale = 0f;
    }

    private void Update()
    {
        Inputs();
        Anim();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Inputs()
    {
        float _x = Input.GetAxisRaw("Horizontal");
        float _y = Input.GetAxisRaw("Vertical");

        direction = new Vector2(_x, _y);
    }

    private void Move()
    {
        rb.velocity = direction * speed * Time.fixedDeltaTime;
    }

    private void Anim()
    {
        anim.SetFloat("Speed_X", direction.x);
        anim.SetFloat("Speed_Y", direction.y);
    }
}
