using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Character character;
    [SerializeField] private Transform respawnPoint;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (character.CharacterLife.Defeated)
            {
                character.transform.localPosition = respawnPoint.position;
                character.ReviveCharacter();
            }
        }
    }
}