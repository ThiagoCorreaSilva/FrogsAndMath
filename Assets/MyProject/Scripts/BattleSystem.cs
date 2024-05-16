using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSystem : MonoBehaviour
{
    [SerializeField] private GameObject battlePanel;
    [SerializeField] private Button attackButton;
    private Enemy enemy;
    private Player player;
    [SerializeField] private bool isPlayerTurn;
    [SerializeField] private bool clicked;
    [SerializeField] private float turnTime;

    private void Awake()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Start()
    {
        battlePanel.SetActive(false);
        attackButton.onClick.AddListener(Attack);

        isPlayerTurn = true;
    }

    private void Update()
    {
        if (enemy.onFight) StartBattle();

        if (player.death)
        {
            battlePanel.SetActive(false);
            isPlayerTurn = false;

            Debug.Log("Player perdeu");
        }
        else if (enemy.death)
        {
            battlePanel.SetActive(false);
            isPlayerTurn = false;

            Debug.Log("Enemy perdeu");
        }
    }

    private void StartBattle()
    {
        battlePanel.SetActive(true);

        StartCoroutine(PlayerTurn());
    }

    private IEnumerator PlayerTurn()
    {
        if (isPlayerTurn && clicked)
        {
            Debug.Log("Player Atacou");

            isPlayerTurn = false;
            attackButton.gameObject.SetActive(false);

            yield return new WaitForSeconds(turnTime);
            StartCoroutine(EnemyTurn());
        }
    }

    private IEnumerator EnemyTurn()
    {
        if (!isPlayerTurn)
        {
            Debug.Log("Enemy Atacou");

            clicked = false;
            isPlayerTurn = true;

            player.TakeDamage(enemy.damage);
            attackButton.gameObject.SetActive(true);

            yield return new WaitForSeconds(turnTime);
            StartCoroutine(PlayerTurn());
        }
    }

    private void Attack()
    {
         enemy.TakeDamage(player.damage);
         clicked = true;
    }
}
