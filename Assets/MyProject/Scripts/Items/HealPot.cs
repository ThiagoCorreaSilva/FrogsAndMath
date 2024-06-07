using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPot : Items
{
    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.gameObject.tag == "Player")
        {
            AddItemOnInventory();
        }
    }
    
    public override void Effect()
    {
        player.GainLife(value);
        Debug.Log("Vida ganha");
    }
}
