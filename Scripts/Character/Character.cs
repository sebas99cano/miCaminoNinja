using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private CharacterStats characterStats;
    public CharacterLife CharacterLife { get; private set; }
    public CharacterChakra CharacterChakra { get; private set; }
    public AnimationCharacter AnimationCharacter { get; set; }

    private void Awake()
    {
        CharacterLife = GetComponent<CharacterLife>();
        CharacterChakra = GetComponent<CharacterChakra>();
        AnimationCharacter = GetComponent<AnimationCharacter>();
    }

    public void ReviveCharacter()
    {
        CharacterLife.ReviveCharacter();
        AnimationCharacter.ReviveCharacter();
        CharacterChakra.RestoreChakraInitial();
    }

    private void AttributeResponse(TypeAttribute typeAttribute)
    {
        if (characterStats.points <= 0)
        {
            return;
        }

        switch (typeAttribute)
        {
            case TypeAttribute.Ninjutsu:
                characterStats.Ninjutsu++;
                characterStats.AddAttributeNinjutsu();
                break;
            case TypeAttribute.Genjutsu:
                characterStats.Genjutsu++;
                characterStats.AddAttributeGenjutsu();
                break;
            case TypeAttribute.Taijutsu:
                characterStats.Taijutsu++;
                characterStats.AddAttributeTaijutsu();
                break;
        }

        characterStats.points--;
    }

    private void OnEnable()
    {
        AttributeButton.EventAddAttribute += AttributeResponse;
    }

    private void OnDisable()
    {
        AttributeButton.EventAddAttribute += AttributeResponse;
    }
}