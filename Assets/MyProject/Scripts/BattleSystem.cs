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
    }

    private void Update()
    {
        if (enemy.onFight) StartBattle();

        attackButton.gameObject.SetActive(isPlayerTurn);

        if (player.death || enemy.death)
        {
            StopAllCoroutines();
            Debug.Log("Alguem ganhou");
        }
    }

    private void StartBattle()
    {
        battlePanel.SetActive(true);
        isPlayerTurn = true;
    }

    private IEnumerator PlayerTurn()
    {
        if (isPlayerTurn)
        {
            yield return new WaitForSeconds(turnTime);
            isPlayerTurn = false;
            StartCoroutine(EnemyTurn());
        }
    }

    private IEnumerator EnemyTurn()
    {
        if (!isPlayerTurn)
        {
            player.TakeDamage(enemy.damage);

            yield return new WaitForSeconds(turnTime);
            isPlayerTurn = true;
            StartCoroutine(PlayerTurn());
        }
    }

    private void Attack()
    {
         GetComponent<Enemy>().TakeDamage(player.damage);
    }
}
