using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class Player : LifeController
{
    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private GameObject joystickPanel;
    private PlatformCheck platformCheck;

    [Header("Movement")]
    public Vector2 direction;
    [SerializeField] private float speed;
    public bool facingLeft;
    public bool canMove;
    [SerializeField] private Joystick joystick;

    [Header("Combat")]
    public float damage;
    public float criticalDamage;
    public bool canSkipQuest;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        platformCheck = GameObject.FindGameObjectWithTag("PlatformCheck").GetComponent<PlatformCheck>();

    }

    protected override void Start()
    {
        base.Start();

        rb.gravityScale = 0f;
        criticalDamage = 2f * damage;

        canMove = true;
        canSkipQuest = true;

        if (!platformCheck.IsOnMobile()) joystickPanel.SetActive(false);
    }

    private void Update()
    {
        Inputs();
        Anim();

        if (direction.x < 0 && !facingLeft || direction.x > 0 && facingLeft || joystick.joystickVec.x < 0 && !facingLeft || joystick.joystickVec.x > 0 && facingLeft) Flip();
    }

    private void FixedUpdate()
    {
        if (!canMove)
            transform.position = Vector2.zero;

        if (!platformCheck.IsOnMobile())
        {
            Move();
            return;
        }

        if (joystick.joystickVec.y != 0 && canMove)
        {
            rb.velocity = new Vector2(joystick.joystickVec.x * speed, joystick.joystickVec.y * speed);
        }
        else if (joystick.joystickVec.y == 0 && canMove)
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void Inputs()
    {
        if (!canMove) return;

        float _x = Input.GetAxisRaw("Horizontal");
        float _y = Input.GetAxisRaw("Vertical");

        direction = new Vector2(_x, _y).normalized;
    }

    private void Move()
    {
        rb.MovePosition(rb.position + direction * speed * Time.deltaTime);
    }

    private void Flip()
    {
        facingLeft = !facingLeft;
        transform.Rotate(Vector2.up * 180f);
    }

    private void Anim()
    {
        if (direction.x != 0 || joystick.joystickVec.x != 0) anim.SetFloat("Speed_X", 1f);
        else anim.SetFloat("Speed_X", 0f);

        anim.SetFloat("Speed_Y", direction.y);
    }
}
