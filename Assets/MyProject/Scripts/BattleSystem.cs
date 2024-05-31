using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    [Header("Puzzle")]
    [SerializeField] TMP_InputField answerInput;
    [SerializeField] Button confirmAnswer;
    [SerializeField] TMP_Text randomQuest;
    [SerializeField] private int result;

    private void Awake()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Start()
    {
        battlePanel.SetActive(false);

        attackButton.onClick.AddListener(Attack);
        confirmAnswer.onClick.AddListener(InputValidation);

        attackButton.gameObject.SetActive(false);
        isPlayerTurn = true;
    }

    private void Update()
    {
        if (enemy.onFight) StartBattle();

        if (player.death)
        {
            battlePanel.SetActive(false);
            StopAllCoroutines();

            Debug.Log("Player perdeu");
        }
        else if (enemy.death)
        {
            battlePanel.SetActive(false);
            StopAllCoroutines();

            Debug.Log("Enemy perdeu");
        }
    }

    #region BattleSystem
    private void StartBattle()
    {
        battlePanel.SetActive(true);

        StartCoroutine(PlayerTurn());

        player.direction.x = 0f;
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

    #endregion

    #region AnswerValidation
    private void InputValidation()
    {
        if (int.TryParse(answerInput.text, out result))
        {
            Debug.Log("Voce acertou");
            attackButton.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("Voce errou");
            attackButton.gameObject.SetActive(false);
            EnemyTurn();
        }
    }

    #endregion
}
