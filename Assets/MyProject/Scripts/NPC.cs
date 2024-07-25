using DialogueEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public NPCConversation dialogue;
    public Canvas dialogueCanvas;
    [SerializeField] private PlatformCheck platform;
    [SerializeField] private GameObject openInventoryHUD;
    [SerializeField] private Inventory inventory;
    [SerializeField] private GameObject mobileHUD;
    [SerializeField] private Button mobileButton;
    [SerializeField] private TMP_Text dialoguePopUp;
    [SerializeField] private Player player;
    [SerializeField] private bool canTalk;
    [SerializeField] private bool already;

    private void Start()
    {
        dialogueCanvas.gameObject.SetActive(true);
        dialoguePopUp.gameObject.SetActive(false);

        mobileButton.onClick.AddListener(StartDialogue);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) StartDialogue();
    }

    private void StartDialogue()
    {
        if (canTalk && !inventory.isOpen && !already)
        {
            ConversationManager.Instance.StartConversation(dialogue);
            dialoguePopUp.gameObject.SetActive(false);

            player.canMove = false;
            already = true;
        }

        ConversationManager.OnConversationEnded += ConversationEnd;
    }

    private void ConversationEnd()
    {
        Invoke(nameof(EnableHUD), 0.4F);

        player.canMove = true;
        already = false;
    }

    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.gameObject.tag == "Player")
        {
            canTalk = true;

            dialoguePopUp.gameObject.SetActive(true);

            if (platform.IsOnMobile())
            {
                dialoguePopUp.text = "Clique no botão para conversar";
                dialoguePopUp.GetComponentInChildren<Button>().gameObject.SetActive(true);
            }
            else
            {
                dialoguePopUp.text = "Aperte E para conversar";
                dialoguePopUp.GetComponentInChildren<Button>().gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D _other)
    {
        if (_other.gameObject.tag == "Player")
        {
            canTalk = false;

            dialoguePopUp.gameObject.SetActive(false);
        }
    }

    public void EnableHUD()
    {
        openInventoryHUD.SetActive(true);
        if (platform.IsOnMobile()) mobileHUD.SetActive(true);
    }

    public void DisableHUD()
    {
        openInventoryHUD.SetActive(false);
        if (platform.IsOnMobile()) mobileHUD.SetActive(false);
    }

    public void StopConversation()
    {
        ConversationManager.Instance.EndConversation();
    }
}