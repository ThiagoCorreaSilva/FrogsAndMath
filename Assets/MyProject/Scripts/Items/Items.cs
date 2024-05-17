using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Items : MonoBehaviour
{
    [SerializeField] protected float value;
    [SerializeField] protected string itemName;
    [SerializeField] private Sprite itemImage;
    [SerializeField] protected bool isUsable;
    private Inventory inventory;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        itemImage = GetComponent<SpriteRenderer>().sprite;
        itemName = gameObject.name;
    }

    protected void Effect()
    {
        gameObject.SetActive(false);

        if (!isUsable) return;

        inventory.AddItem(itemName, itemImage);
    }
}
