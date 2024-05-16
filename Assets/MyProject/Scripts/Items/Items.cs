using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    [SerializeField] protected float value;
    [SerializeField] protected bool isUsable;
    public Sprite sprite;
    private Inventory inventory;

    protected void Effect()
    {
        for (int i = 0; i < inventory.inventoryOccupied.Length; i++)
        {
            if (inventory.inventoryOccupied[i] == false)
            {
                inventory.inventoryOccupied[i] = true;
                Debug.Log("Deu certo");
            }
            else
            {
                Debug.Log("Inventario cheio");
            }
        }
    }
}
