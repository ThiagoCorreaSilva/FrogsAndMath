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
    [SerializeField] private bool[] inventoryOccupied;
    [SerializeField] private int previousItem;
    [SerializeField] private int[] itensIndex;
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

    public void AddItem(int _index, Sprite _newImage)
    {
        for (int i = 0; i < inventoryOccupied.Length; i++)
        {
            if (inventoryOccupied[i] == false)
            {
                if (previousItem == _index)
                {
                    foreach (var _newIndex in itensIndex)
                    {
                        if (previousItem == _index || _newIndex == _index)
                        {
                            Debug.Log("Stack");

                            previousItem = _index;
                            break;
                        }
                    }
                }
                else
                {
                    Debug.Log("Novo item");

                    inventoryOccupied[i] = true;
                    slots[i].GetComponent<Image>().sprite = _newImage;
                    previousItem = _index;
                }
                return;

            }
        }
    }
}

