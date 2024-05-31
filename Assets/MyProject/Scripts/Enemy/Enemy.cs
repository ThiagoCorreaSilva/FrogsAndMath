using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class Enemy : LifeController
{
    private Rigidbody2D rb;
    private Animator anim;
    private Player player;

    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float distanceToFollow;

    [Header("Fight Variables")]
    public float damage;
    [SerializeField] private float criticalDamage;
    [SerializeField] private GameObject popUp;
    [SerializeField] private GameObject fadeIn;
    public bool onFight;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    protected override void Start()
    {
        base.Start();

        rb.gravityScale = 0f;
        rb.freezeRotation = true;

        fadeIn.SetActive(false);
        popUp.SetActive(false);
    }

    private void Update()
    {
        DisToPlayer();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private float DisToPlayer()
    {
        return Vector2.Distance(transform.position, player.transform.position);
    }

    private void Move()
    {
        if (DisToPlayer() <= distanceToFollow && !onFight)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.fixedDeltaTime);

            popUp.SetActive(true);
        }
        else
            popUp.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D _other)
    {
        if (_other.gameObject.tag == "Player")
        {
            onFight = true;
            player.canMove = false;
            fadeIn.SetActive(true);

            transform.localPosition -= new Vector3(1.2f, 0f, 0f);
            speed = 0f;
        }
    }

    public override void Death()
    {
        base.Death();

        player.canMove = true;

        Destroy(gameObject);

        Debug.Log("Morri");
    }
}
