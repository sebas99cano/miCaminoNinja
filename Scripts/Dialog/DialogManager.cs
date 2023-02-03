using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : Singleton<DialogManager>
{
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private Image npcIcon;
    [SerializeField] private TextMeshProUGUI npcName;
    [SerializeField] private TextMeshProUGUI npcChat;

    private bool animateDialog;
    private bool showGoodbye;
    public NPCInteraction NpcDisposable { get; set; }

    private Queue<string> _sequenceChat;

    private void Start()
    {
        _sequenceChat = new Queue<string>();
    }

    private void Update()
    {
        if (NpcDisposable == null)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.E) && dialogPanel.activeSelf == false)
        {
            ConfigPanel(NpcDisposable.Dialog);
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (showGoodbye)
            {
                OpenCloseDialogPanel(false);
                showGoodbye = false;
                return;
            }

            if (animateDialog)
            {
                ContinueDialog();
            }
        }
    }

    public void OpenCloseDialogPanel(bool state)
    {
        dialogPanel.SetActive(state);
    }

    private void ConfigPanel(NPCDialog npcDialog)
    {
        OpenCloseDialogPanel(true);
        LoadDialogSequence(npcDialog);
        npcIcon.sprite = npcDialog.icon;
        npcName.text = $"{npcDialog.nameNPC}:";
        ShowAnimatedTex(npcDialog.salute);
    }

    private void LoadDialogSequence(NPCDialog npcDialog)
    {
        if (npcDialog.Conversation == null || npcDialog.Conversation.Length <= 0)
        {
            return;
        }

        for (int i = 0; i < npcDialog.Conversation.Length; i++)
        {
            _sequenceChat.Enqueue(npcDialog.Conversation[i].Sentence);
        }
    }

    private IEnumerator AnimateText(string sentence)
    {
        animateDialog = false;
        npcChat.text = "";
        char[] letters = sentence.ToCharArray();
        for (int i = 0; i < letters.Length; i++)
        {
            npcChat.text += letters[i];
            yield return new WaitForSeconds(0.03f);
        }

        animateDialog = true;
    }

    private void ShowAnimatedTex(string sentence)
    {
        StartCoroutine(AnimateText(sentence));
    }

    private void ContinueDialog()
    {
        if (NpcDisposable == null)
        {
            return;
        }

        if (showGoodbye)
        {
            return;
        }

        if (_sequenceChat.Count == 0)
        {
            string goodbye = NpcDisposable.Dialog.goodbye;
            ShowAnimatedTex(goodbye);
            showGoodbye = true;
            return;
        }

        string nextDialog = _sequenceChat.Dequeue();
        ShowAnimatedTex(nextDialog);
    }
}