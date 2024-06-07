using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Items : MonoBehaviour
{
    [SerializeField] protected float value;
    [SerializeField] protected bool isUsable;
    [SerializeField] protected Player player;
    private Inventory inventory;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public virtual void Effect()
    {

    }

    protected void AddItemOnInventory()
    {
        if (!isUsable || inventory.isFull) return;

        inventory.AddItem(gameObject);
        gameObject.SetActive(false);
    }
}