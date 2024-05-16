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
    private Player player;

    private void Start()
    {
        openInventory.onClick.AddListener(OpenInventory);
        closeInventory.onClick.AddListener(CloseInventory);

        inventoryPanel.SetActive(false);
        closeInventory.gameObject.SetActive(false);

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OpenInventory()
    {
        inventoryPanel.SetActive(true);
        openInventory.gameObject.SetActive(false);
        closeInventory.gameObject.SetActive(true);
    }

    private void CloseInventory()
    {
        inventoryPanel.SetActive(false);
        openInventory.gameObject.SetActive(true);
        closeInventory.gameObject.SetActive(false);
    }

    private void UseItem()
    {
        Debug.Log("Usou item");
    }
}
