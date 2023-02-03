using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NPCInteractionExtra
{
    Quest,
    Shop,
    Crafting
}
[CreateAssetMenu]
public class NPCDialog : ScriptableObject
{
    [Header("Info")] public string nameNPC;
    public Sprite icon;
    public bool hasInteractionExtra;
    public NPCInteractionExtra interactionExtra;

    [Header("Salute")] [TextArea] public string salute;

    [Header("Chat")] public DialogText[] Conversation;

    [Header("Goodbye")] [TextArea] public string goodbye;

}
[Serializable]
public class DialogText
{
    [TextArea] public string Sentence;
}
