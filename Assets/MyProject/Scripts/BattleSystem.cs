using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSystem : MonoBehaviour
{
    [SerializeField] private GameObject battlePanel;
    [SerializeField] private Button attackButton;
    private Enemy enemy;

    private void Awake()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
    }

    private void Start()
    {
        battlePanel.SetActive(false);
        attackButton.onClick.AddListener(Attack);
    }

    private void Update()
    {
        if (enemy.onFight) StartBattle();
    }

    private void StartBattle()
    {
        battlePanel.SetActive(true);
    }

    private void Attack()
    {
        Debug.Log("Ataquei");
    }
}
