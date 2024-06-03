using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSizeEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Vector3 newSize = new Vector3(1.7f , 1.7f, 1.7f);
    private Vector3 initialSize;

    private void Start()
    {
        initialSize = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = newSize;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = initialSize;
    }
}
