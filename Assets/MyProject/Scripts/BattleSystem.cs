using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleSystem : MonoBehaviour
{
    [SerializeField] private Camera cam;

    [SerializeField] private GameObject battlePanel;
    [SerializeField] private Button attackButton;
    [SerializeField] private Button skipButton;
    private Enemy enemy;
    private Player player;
    [SerializeField] private bool isPlayerTurn;
    [SerializeField] private bool clicked;
    [SerializeField] private float turnTime;
    [SerializeField] private TMP_Text winText;

    [Header("Puzzle")]
    [SerializeField] TMP_InputField answerInput;
    [SerializeField] Button confirmAnswer;
    [SerializeField] TMP_Text quests;
    [SerializeField] private char[] operations;
    [SerializeField] private int minNumber;
    [SerializeField] private int maxNumber;
    [SerializeField] private int result;

    [Header("Critical Attack")]
    [SerializeField] private float criticalTime;
    [SerializeField] private float timeElapsed;
    [SerializeField] private bool canCritical;
    
    private void Awake()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Start()
    {
        battlePanel.SetActive(false);
        answerInput.gameObject.SetActive(false);
        skipButton.gameObject.SetActive(player.canSkipQuest);
        winText.gameObject.SetActive(false);

        attackButton.gameObject.SetActive(true);

        attackButton.onClick.AddListener(Attack);
        skipButton.onClick.AddListener(SkipQuest);
        confirmAnswer.onClick.AddListener(InputValidation);

        RandomQuest();
    }

    private void Update()
    {
        if (enemy.onFight)
        {
            StartBattle();
            timeElapsed += Time.deltaTime;
        }

        if (player.death)
        {
            battlePanel.SetActive(false);

            StopAllCoroutines();

            Debug.Log("Player perdeu");
        }
        else if (enemy.death)
        {
            battlePanel.SetActive(false);
            winText.gameObject.SetActive(true);

            enemy.death = false;

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

            if (canCritical)
            {
                Debug.Log("CRITICO");

                enemy.TakeDamage(player.criticalDamage);
            }
            else
                enemy.TakeDamage(player.damage);

            canCritical = false;

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
            canCritical = false;

            player.TakeDamage(enemy.damage);
            cam.GetComponent<CameraFollow>().CameraShake();

            yield return new WaitForSeconds(turnTime);
            attackButton.gameObject.SetActive(true);
        }
    }

    private void Attack()
    {
        attackButton.gameObject.SetActive(false);

        answerInput.gameObject.SetActive(true);
        skipButton.gameObject.SetActive(player.canSkipQuest);

        timeElapsed = 0f;
    }

    #endregion

    #region AnswerValidation
    private void InputValidation()
    {
        // Converte a string em int
        if (int.TryParse(answerInput.text, out int _playerResult))
        {
            // Verifica se a resposta do player esta certa
            if (_playerResult == result)
            {
                Debug.Log("Voce acertou");

                answerInput.text = null;

                answerInput.gameObject.SetActive(false);

                isPlayerTurn = true;
                clicked = true;

                if (timeElapsed < criticalTime)
                    canCritical = true;
                else
                    canCritical = false;

                StartCoroutine(PlayerTurn());
                RandomQuest();
            }
            else
            {
                Debug.Log("Voce errou");

                answerInput.text = null;

                isPlayerTurn = false;
                clicked = false;

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

        // Garantir que o primeiro numero sera maior
        while (_randomNumber1 < _randomNumber2)
        {
            _randomNumber1 = Random.Range(minNumber, maxNumber);
        }

        // Verifica qual operation foi escolhida
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

                // Garante que o primeiro e o segundo numeros sao pares
                while (_randomNumber1 % 2 != 0)
                {
                    _randomNumber1 = Random.Range(minNumber, maxNumber);
                }

                while (_randomNumber2 % 2 != 0 && _randomNumber2 > _randomNumber1)
                {
                    _randomNumber2 = Random.Range(minNumber, maxNumber);
                }

                result = Mathf.RoundToInt(_randomNumber1 / _randomNumber2);
                break;
        }

        // Atualiza o texto de quest
        quests.text = _randomNumber1.ToString() + " " + operations[_randomOperation].ToString() + " " + _randomNumber2.ToString();
    }

    private void SkipQuest()
    {
        if (!player.canSkipQuest) return;

        RandomQuest();

        player.canSkipQuest = false;
        skipButton.gameObject.SetActive(player.canSkipQuest);
    }

    #endregion
}