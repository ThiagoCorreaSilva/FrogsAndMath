using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Button gameButton;
    [SerializeField] private Button exitButton;

    private void Start()
    {
        gameButton.onClick.AddListener(GameButton);
        exitButton.onClick.AddListener(ExitButton);
    }

    private void GameButton()
    {
        SceneManager.LoadScene("Game");
    }

    private void ExitButton()
    {
        Application.Quit();
    }
}