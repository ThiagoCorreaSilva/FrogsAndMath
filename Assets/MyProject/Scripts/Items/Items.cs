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
    [SerializeField] private int previousItem;
    [SerializeField] private int[] itensIndex;
    private Inventory inventory;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        itemImage = GetComponent<SpriteRenderer>().sprite;
    }

    protected void Effect()
    {
        if (!isUsable) return;

        for (int i = 0; i < inventory.inventoryOccupied.Length; i++)
        {
            if (inventory.inventoryOccupied[i] == false)
            {
                gameObject.SetActive(false);

                if (previousItem == index)
                {
                    Debug.Log("Real stack");
                    previousItem = index;
                    break;
                }

                foreach (var _index in itensIndex)
                {  
                    if (_index == index)
                    {
                        Debug.Log("Foreach stack");
                        break;
                    }
                }
                inventory.inventoryOccupied[i] = true;
                inventory.slots[i].GetComponent<Image>().sprite = itemImage;

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
