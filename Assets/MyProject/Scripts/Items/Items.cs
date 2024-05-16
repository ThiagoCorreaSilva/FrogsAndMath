using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Items : MonoBehaviour
{
    [SerializeField] protected float value;
    [SerializeField] protected bool isUsable;
    private Image itemSprite;
    private Inventory inventory;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        itemSprite = GetComponent<Image>();
    }

    protected void Effect()
    {
        if (!isUsable) return;

        for (int i = 0; i < inventory.inventoryOccupied.Length; i++)
        {
            if (inventory.inventoryOccupied[i] == false)
            {
                inventory.inventoryOccupied[i] = true;
                inventory.slots[i].image = itemSprite;
                gameObject.SetActive(false);

                Debug.Log("Deu certo");
                return;
            }
            else
            {
                Debug.Log("Inventario cheio");
            }
        }
    }
}
