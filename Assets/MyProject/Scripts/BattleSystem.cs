using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSystem : MonoBehaviour
{
    [SerializeField] private GameObject battlePanel;
    private Enemy enemy;

    private void Awake()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
    }

    private void Start()
    {
        battlePanel.SetActive(false);
    }

    private void Update()
    {
        if (enemy.onFight) StartBattle();
    }

    private void StartBattle()
    {
        battlePanel.SetActive(true);

    }
}
