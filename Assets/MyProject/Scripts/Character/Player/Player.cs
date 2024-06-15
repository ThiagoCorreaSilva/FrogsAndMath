using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private float jumpForce;
    [SerializeField] private Transform groundPos;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundRadius;
    public bool facingLeft;
    public bool canMove;
    private bool draginLeft;
    private bool draginRight;
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    [SerializeField] private Button jumpButton;

    [Header("Combat")]
    public float damage;
    public float criticalDamage;
    public bool canSkipQuest;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        platformCheck = GameObject.FindGameObjectWithTag("PlatformCheck").GetComponent<PlatformCheck>();

        jumpButton.onClick.AddListener(JumpButton);
    }

    protected override void Start()
    {
        base.Start();

        criticalDamage = 2f * damage;

        canMove = true;
        canSkipQuest = true;

        if (!platformCheck.IsOnMobile()) joystickPanel.SetActive(false);
    }

    protected override void Update()
    {
        base.Update();

        OnGround();
        Inputs();
        Anim();

        if (direction.x < 0 && !facingLeft || direction.x > 0 && facingLeft) Flip();
    }

    private void FixedUpdate()
    {
        if (!canMove)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        Move();
    }

    private void Inputs()
    {
        if (!canMove) return;

        float _x = Input.GetAxisRaw("Horizontal");

        direction = new Vector2(_x, 0f).normalized;
        

        if (Input.GetKeyDown(KeyCode.Space) && OnGround()) Jump();
    }

    private void Move()
    {
        if (!draginLeft && !draginRight)
            rb.velocity = new Vector2(direction.x * speed * Time.deltaTime, rb.velocity.y);

        else if (draginRight && !draginLeft)
        {
            rb.velocity = new Vector2(direction.x = 1 * speed * Time.deltaTime, rb.velocity.y);

            if (facingLeft) Flip();
        }

        else if (draginLeft && !draginRight)
        {
            rb.velocity = new Vector2(direction.x = -1 * speed * Time.deltaTime, rb.velocity.y);

            if (!facingLeft) Flip();
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private bool OnGround()
    {
        return Physics2D.OverlapCircle(

            groundPos.position,
            groundRadius,
            groundLayer

            );
    }

    #region Mobile Movement
    public void LeftButton()
    {
        draginLeft = true;
    }

    public void RightButton()
    {
        draginRight = true;
    }

    private void JumpButton()
    {
        if (OnGround())
            Jump();
    }

    public void DragEnd()
    {
        draginRight = false;
        draginLeft = false;
    }

    #endregion

    private void Flip()
    {
        facingLeft = !facingLeft;
        transform.Rotate(Vector2.up * 180f);
    }

    private void Anim()
    {
        if (rb.velocity.x != 0) anim.SetFloat("Speed_X", 1f);
        else anim.SetFloat("Speed_X", 0f);
    }
}
