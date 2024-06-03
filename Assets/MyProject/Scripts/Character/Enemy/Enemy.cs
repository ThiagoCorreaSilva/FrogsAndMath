using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class Enemy : LifeController
{
    private Rigidbody2D rb;
    private Animator anim;
    private Player player;
    private BattleSystem battleSystem;

    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float distanceToFollow;

    [Header("Fight Variables")]
    public float damage;
    [SerializeField] private float criticalDamage;
    [SerializeField] private GameObject popUp;
    public GameObject fadeIn;
    public bool onFight;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        battleSystem = GameObject.FindGameObjectWithTag("BattleSystem").GetComponent<BattleSystem>();
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
            battleSystem.enemy = gameObject;

            battleSystem.StartBattle();

            fadeIn.SetActive(true);

            speed = 0f;
        }
    }

    public override void Death()
    {
        base.Death();

        player.canMove = true;

        gameObject.SetActive(false);
        Destroy(gameObject, 2f);

        Debug.Log("Morri");
    }
}