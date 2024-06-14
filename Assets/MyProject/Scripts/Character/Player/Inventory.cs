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
    [SerializeField] private List<GameObject> itemsList = new();
    [SerializeField] private GameObject joystickPanel;
    private PlatformCheck platformCheck;
    private bool itemAdded;
    public bool isFull;
    private Player player;

    private void Start()
    {
        openInventory.onClick.AddListener(OpenInventory);
        closeInventory.onClick.AddListener(CloseInventory);

        inventoryPanel.SetActive(false);

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        platformCheck = GameObject.FindGameObjectWithTag("PlatformCheck").GetComponent<PlatformCheck>();

        for (int i = 0; i < slots.Length; i++)
        {
            int _index = i;
            slots[i].onClick.AddListener(() => UseItem(_index));
        }
    }

    private void Update()
    {
        if (itemsList.Count < slots.Length)
        {
            isFull = false;
        }
    }

    private void OpenInventory()
    {
        inventoryPanel.SetActive(true);
        openInventory.gameObject.SetActive(false);

        player.canMove = false;
        player.direction = new Vector3(0f, 0f , 0f);

        if (platformCheck.IsOnMobile()) joystickPanel.SetActive(false);
    }

    private void CloseInventory()
    {
        inventoryPanel.SetActive(false);
        openInventory.gameObject.SetActive(true);

        player.canMove = true;

        if (platformCheck.IsOnMobile()) joystickPanel.SetActive(true);
    }

    public void AddItem(GameObject _itemObject)
    {
        if (itemsList.Count >= slots.Length)
        {
            isFull = true;

            Debug.Log("Inventario cheio");
            return;
        }
        else if (IsOnList(_itemObject))
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
                    itemsList[i] = _itemObject;
                    slots[i].GetComponent<Image>().sprite = _itemObject.GetComponent<SpriteRenderer>().sprite;
                    
                    Color _color = slots[i].GetComponent<Image>().color;
                    _color.a = 1f;
                    slots[i].GetComponent<Image>().color = _color;

                    itemAdded = true;
                    break;
                }
            }

            if (!itemAdded)
            {
                itemsList.Add(_itemObject);
                slots[itemsList.IndexOf(_itemObject)].GetComponent<Image>().sprite = _itemObject.GetComponent<SpriteRenderer>().sprite;

                Color _color = slots[itemsList.IndexOf(_itemObject)].GetComponent<Image>().color;
                _color.a = 1f;
                slots[itemsList.IndexOf(_itemObject)].GetComponent<Image>().color = _color;
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

        Debug.Log($"Usando o Item: {itemsList[_slotIndex].name}");
        itemsList[_slotIndex].GetComponent<Items>().Effect();

        itemsList.RemoveAt(_slotIndex);
        slots[_slotIndex].GetComponent<Image>().sprite = default;

        Color _color = slots[_slotIndex].GetComponent<Image>().color;
        _color.a = 0f;
        slots[_slotIndex].GetComponent<Image>().color = _color;

        // Desloca os itens subsequentes para preencher o espaço vazio
        for (int i = _slotIndex; i < itemsList.Count; i++)
        {
            slots[i].GetComponent<Image>().sprite = slots[i + 1].GetComponent<Image>().sprite;
        }

        // Limpa o último slot
        slots[itemsList.Count].GetComponent<Image>().sprite = default;
    }

    private bool IsOnList(GameObject _obj)
    {
        foreach (GameObject _item in itemsList)
        {
            if (_item.name == _obj.name)
                return true;
        }

        return false;
    }
}