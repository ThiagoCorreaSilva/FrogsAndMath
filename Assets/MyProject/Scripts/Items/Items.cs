using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Items : MonoBehaviour
{
    [SerializeField] protected float value;
    [SerializeField] protected int index;
    [SerializeField] protected bool isUsable;
    [SerializeField] private Sprite itemImage;

    private Inventory inventory;
    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        itemImage = GetComponent<SpriteRenderer>().sprite;
    }

    protected void Effect()
    {
        gameObject.SetActive(false);

        if (!isUsable) return;

        inventory.AddItem(index, itemImage);
    }
}
