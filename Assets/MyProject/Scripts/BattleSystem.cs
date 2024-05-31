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
    [SerializeField] TMP_Text quests;
    [SerializeField] private char[] operations;
    [SerializeField] private int minNumber;
    [SerializeField] private int maxNumber;
    [SerializeField] private int result;

    private void Awake()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Start()
    {
        battlePanel.SetActive(false);
        attackButton.gameObject.SetActive(false);

        attackButton.onClick.AddListener(Attack);
        confirmAnswer.onClick.AddListener(InputValidation);

        RandomQuest();
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

            yield return new WaitForSeconds(turnTime);
            StartCoroutine(PlayerTurn());
            answerInput.gameObject.SetActive(true);
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
        if (int.TryParse(answerInput.text, out int _playerResult))
        {
            if (_playerResult == result)
            {
                Debug.Log("Voce acertou");

                isPlayerTurn = true;

                attackButton.gameObject.SetActive(true);
                answerInput.gameObject.SetActive(false);

                RandomQuest();
            }
            else
            {
                Debug.Log("Voce errou");

                isPlayerTurn = false;

                attackButton.gameObject.SetActive(false);
                answerInput.gameObject.SetActive(false);

                StartCoroutine(EnemyTurn());
                RandomQuest();
            }
        }
        else
        {
            Debug.LogError("Nao foi possivel converter");
        }
    }

    private void RandomQuest()
    {
        int _randomNumber1 = Random.Range(minNumber, maxNumber);
        int _randomNumber2 = Random.Range(minNumber, maxNumber);
        int _randomOperation = Random.Range(0, operations.Length);

        switch (operations[_randomOperation])
        {
            case '+':
                result = _randomNumber1 + _randomNumber2;
                 break;

            case '-':
                result = _randomNumber1 - _randomNumber2;
                break;

            case '*':
                result = _randomNumber1 * _randomNumber2;
                break;

            case '/':
                result = _randomNumber1 / _randomNumber2;
                break;
        }

        quests.text = _randomNumber1.ToString() + operations[_randomOperation].ToString() + _randomNumber2.ToString();
    }

    #endregion
}
