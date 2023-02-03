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
   //[SerializeField] private TextMeshProUGUI npcChat;

   public NPCInteraction NpcDisposable { get; set; }

   private void Update()
   {
      if (NpcDisposable == null)
      {
         return;
      }

      if (Input.GetKeyDown(KeyCode.E))
      {
         ConfigPanel(NpcDisposable.Dialog);
      }
   }

   public void OpenCloseDialogPanel(bool state)
   {
      dialogPanel.SetActive(state);
   }

   private void ConfigPanel(NPCDialog npcDialog)
   {
      OpenCloseDialogPanel(true);
      npcIcon.sprite = npcDialog.icon;
      npcName.text = npcDialog.nameNPC;
      
   }
}
