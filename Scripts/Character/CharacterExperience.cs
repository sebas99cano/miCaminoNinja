using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterExperience : MonoBehaviour
{
    [Header("Stats")] [SerializeField] private CharacterStats characterStats;

    [Header("Configuration")] [SerializeField]
    private float maxLevel;

    [SerializeField] private float baseExperience;
    [SerializeField] private float incrementalExperienceDifficult;
    
    private float _actualExperienceTemporal;
    private float _experienceValueToNextLevel;

    // Start is called before the first frame update
    void Start()
    {
        characterStats.Level = 1;
        _experienceValueToNextLevel = baseExperience;
        characterStats.NeededExperience = _experienceValueToNextLevel;
        UpdateExperienceBar();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            AddExperience(10f);
        }
    }

    public void AddExperience(float obtainedExperience)
    {
        if (obtainedExperience > 0f)
        {
            float remainingExperience = _experienceValueToNextLevel - _actualExperienceTemporal;
            if (obtainedExperience >= remainingExperience)
            {
                obtainedExperience -= remainingExperience;
                UpdateLevel();
                AddExperience(obtainedExperience);
            }
            else
            {
                _actualExperienceTemporal += obtainedExperience;
                if (_actualExperienceTemporal == _experienceValueToNextLevel)
                {
                    UpdateLevel();
                }
            }
        }

        characterStats.Experience = _actualExperienceTemporal;
        UpdateExperienceBar();
    }

    private void UpdateLevel()
    {
        if (characterStats.Level < maxLevel)
        {
            characterStats.Level++;
            _actualExperienceTemporal = 0f;
            _experienceValueToNextLevel *= incrementalExperienceDifficult;
            characterStats.NeededExperience = _experienceValueToNextLevel;
            characterStats.points += 2;
            characterStats.Experience = _actualExperienceTemporal;
        }
    }

    private void UpdateExperienceBar()
    {
        UIManager.Instance.UpdateCharacterExperience(_actualExperienceTemporal, _experienceValueToNextLevel);
    }
}