using DialogueEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public NPCConversation dialogue;
    [SerializeField] private PlatformCheck platform;
    [SerializeField] private GameObject inventoryHUD;
    [SerializeField] private GameObject mobileHUD;
    [SerializeField] private bool canTalk;

    private void Update()
    {
        if (canTalk && Input.GetKeyDown(KeyCode.E))
        {
            ConversationManager.Instance.StartConversation(dialogue);
        }
    }

    private void OnCollisionEnter2D(Collision2D _other)
    {
        if (_other.gameObject.tag == "Player")
            canTalk = true;
    }

    private void OnCollisionExit2D(Collision2D _other)
    {
        if (_other.gameObject.tag == "Player")
            canTalk = false;
    }

    public void EnableInventory()
    {
        inventoryHUD.SetActive(true);

        if (platform.IsOnMobile()) mobileHUD.SetActive(true);
    }

    public void DisableInventory()
    {
        inventoryHUD.SetActive(false);

        if (platform.IsOnMobile()) mobileHUD.SetActive(false);
    }
}