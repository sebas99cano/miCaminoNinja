using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeAttribute
{
    Ninjutsu,
    Genjutsu,
    Taijutsu,
}

public class AttributeButton : MonoBehaviour
{

    public static Action<TypeAttribute> EventAddAttribute;

    [SerializeField] private TypeAttribute TypeAttribute;

    public void AddAttribute()
    {
        EventAddAttribute?.Invoke(TypeAttribute);
    }
}