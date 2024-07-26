using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneIntro : MonoBehaviour
{
    [SerializeField] private float sceneTime;
    [SerializeField] private string sceneName;

    private void Update()
    {
        sceneTime -= Time.deltaTime;

        if (sceneTime <= 0)
            SceneManager.LoadScene(sceneName);
    }
}