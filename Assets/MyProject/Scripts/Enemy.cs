using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private Transform player;

    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float distanceToFollow;

    [Header("PopUp")]
    [SerializeField] private GameObject popUp;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Start()
    {
        rb.gravityScale = 0f;

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
        return Vector2.Distance(transform.position, player.position);
    }

    private void Move()
    {
        if (DisToPlayer() <= distanceToFollow)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.fixedDeltaTime);

            popUp.SetActive(true);
        }
        else popUp.SetActive(false);
    }
}
