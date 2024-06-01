using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableGameObject : MonoBehaviour
{
    [SerializeField] private GameObject fadeIn;
    
    public void Disable()
    {
        if (fadeIn == null)
            gameObject.SetActive(false);
        else
            fadeIn.SetActive(false);
    }
}
