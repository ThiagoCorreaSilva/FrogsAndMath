using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Button inventoryButton;
    [SerializeField] private GameObject inventoryPanel;
    private Player player;

    private void Start()
    {
        inventoryPanel.SetActive(false);

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        inventoryButton.onClick.AddListener(OpenInventory);
    }

    private void OpenInventory()
    {
        inventoryPanel.SetActive(true);
    }
}
