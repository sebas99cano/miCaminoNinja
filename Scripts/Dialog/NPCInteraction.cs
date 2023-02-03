using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    [SerializeField] private GameObject npcButtonInteract;
    [SerializeField] private NPCDialog npcDialog;
    [SerializeField] private WaypointMovement npcMovement;

    public NPCDialog Dialog => npcDialog;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            npcMovement.isWaiting=true;
            DialogManager.Instance.NpcDisposable = this;
            npcButtonInteract.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            npcMovement.isWaiting = false;
            DialogManager.Instance.NpcDisposable = null;
            npcButtonInteract.SetActive(false);
        }
    }
}
