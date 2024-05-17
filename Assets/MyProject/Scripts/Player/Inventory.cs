using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Button openInventory;
    [SerializeField] private Button closeInventory;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private Button[] slots;
    [SerializeField] private List<string> itemsName;

    private Player player;

    private void Start()
    {
        openInventory.onClick.AddListener(OpenInventory);
        closeInventory.onClick.AddListener(CloseInventory);

        inventoryPanel.SetActive(false);

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].onClick.AddListener(UseItem);
        }
    }

    private void OpenInventory()
    {
        inventoryPanel.SetActive(true);
        openInventory.gameObject.SetActive(false);

        player.canMove = false;
    }

    private void CloseInventory()
    {
        inventoryPanel.SetActive(false);
        openInventory.gameObject.SetActive(true);

        player.canMove = true;
    }

    private void UseItem()
    {
        Debug.Log("Usou item");
    }

    public void AddItem(string _itemName, Sprite _newImage)
    {
        if (itemsName.Contains(_itemName))
        {
            Debug.Log("Stackado");
        }
        else
        {
            itemsName.Add(_itemName);

            slots[itemsName.IndexOf(_itemName)].GetComponent<Image>().sprite = _newImage;
            Debug.Log("Novo Item");
        }
    }
}

