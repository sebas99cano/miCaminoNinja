using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CharacterStats")]
public class CharacterStats : ScriptableObject
{
    [Header("Stats")] public float Damage = 5f;
    public float Defense = 2f;
    public float Chakra;
    [Range(0f, 100f)] public float Dodge;
    public float Velocity = 4f;
    public float Level;
    public float Experience;
    public float NeededExperience;

    [Header("Attributes")] public int Ninjutsu;
    public int Genjutsu;
    public int Taijutsu;
    [HideInInspector] public int points;

    public void AddAttributeNinjutsu()
    {
        Chakra += 4f;
        Dodge += 1;
        Defense += 1;
    }

    public void AddAttributeGenjutsu()
    {
        Chakra += 2;
        Dodge += 6;
        Defense += 1;
    }

    public void AddAttributeTaijutsu()
    {
        Damage += 3;
        Defense += 2;
        Dodge += 4;
    }

    public void ResetValues()
    {
        Damage = 5f;
        Defense = 2f;
        Chakra = 3f;
        Dodge = 1f;
        Velocity = 4f;
        Level = 1f;
        Experience = 0f;
        NeededExperience = 10f;

        Ninjutsu = 0;
        Genjutsu = 0;
        Taijutsu = 0;

        points = 0;
    }
}