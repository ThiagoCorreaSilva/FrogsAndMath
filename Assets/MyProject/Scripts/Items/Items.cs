using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Items : MonoBehaviour
{
    [SerializeField] protected float value;
    [SerializeField] protected bool isUsable;
    [SerializeField] protected Player player;
    private Inventory inventory;

    [Header("Up and Down")]
    [SerializeField] protected float amplitude;
    [SerializeField] protected float frequency;
    [SerializeField] protected bool haveEffect;
    protected Vector2 initialPos;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        initialPos = transform.position;
    }

    protected virtual void Update()
    {
        if (haveEffect)
        {
            Vector3 _newPos = initialPos;
            _newPos.y += Mathf.Sin(Time.timeSinceLevelLoad * Mathf.PI * frequency) * amplitude;
            transform.position = _newPos;
        }
    }

    public virtual void Effect()
    {

    }

    protected void AddItemOnInventory()
    {
        if (!isUsable || inventory.isFull) return;

        inventory.AddItem(gameObject);
        gameObject.SetActive(false);
    }
}