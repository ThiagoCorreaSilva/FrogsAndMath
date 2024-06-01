using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Button openInventory;
    [SerializeField] private Button closeInventory;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private Button[] slots;
    [SerializeField] private List<string> itemsList = new();
    private bool itemAdded;
    public bool isFull;
    private Player player;

    private void Start()
    {
        openInventory.onClick.AddListener(OpenInventory);
        closeInventory.onClick.AddListener(CloseInventory);

        inventoryPanel.SetActive(false);

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        for (int i = 0; i < slots.Length; i++)
        {
            int _index = i;
            slots[i].onClick.AddListener(() => UseItem(_index));
        }
    }

    private void OpenInventory()
    {
        inventoryPanel.SetActive(true);
        openInventory.gameObject.SetActive(false);

        player.canMove = false;
        player.direction = new Vector3(0f, 0f , 0f);
    }

    private void CloseInventory()
    {
        inventoryPanel.SetActive(false);
        openInventory.gameObject.SetActive(true);

        player.canMove = true;
    }

    private void Update()
    {
        if (itemsList.Count < slots.Length)
        {
            isFull = false;
        }
    }

    public void AddItem(string _itemName, Sprite _itemImage)
    {

        if (itemsList.Count >= slots.Length)
        {
            isFull = true;

            Debug.Log("Inventario cheio");
            return;
        }
        else if (itemsList.Contains(_itemName))
        {
            Debug.Log("Stackado");
        }
        else
        {
            Debug.Log("Novo Item");

            for (int i = 0; i < itemsList.Count; i++)
            {
                if (itemsList[i] == null)
                {
                    itemsList[i] = _itemName;
                    slots[i].GetComponent<Image>().sprite = _itemImage;
                    itemAdded = true;
                    break;
                }
            }

            if (!itemAdded)
            {
                itemsList.Add(_itemName);
                slots[itemsList.IndexOf(_itemName)].GetComponent<Image>().sprite = _itemImage;
            }
        }
    }

    private void UseItem(int _slotIndex)
    {
        if (_slotIndex >= itemsList.Count || itemsList.Count == 0)
        {
            Debug.LogError("Fora de alcance");
            return;
        }

        Debug.Log($"Usando item: {itemsList[_slotIndex]}");

        itemsList.RemoveAt(_slotIndex);
        slots[_slotIndex].GetComponent<Image>().sprite = default;

        // Desloca os itens subsequentes para preencher o espaço vazio
        for (int i = _slotIndex; i < itemsList.Count; i++)
        {
            slots[i].GetComponent<Image>().sprite = slots[i + 1].GetComponent<Image>().sprite;
        }

        // Limpa o último slot
        slots[itemsList.Count].GetComponent<Image>().sprite = default;
    }
}