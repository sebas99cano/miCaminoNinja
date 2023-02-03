using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterChakra : MonoBehaviour
{
    [SerializeField] private float initialChakra;
    [SerializeField] private float maxChakra;
    [SerializeField] private float regenerationBySecond;

    public float ActualChakra { get; private set; }

    public bool CanBeRestoreChakra => ActualChakra < maxChakra;

    private CharacterLife _characterLife;

    private void Awake()
    {
        _characterLife = GetComponent<CharacterLife>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ActualChakra = initialChakra;
        UpdateChakraBar();

        InvokeRepeating(nameof(RegenerateChakra), 1, 1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            UseChakra(10f);
        }
    }

    public void UseChakra(float amount)
    {
        if (ActualChakra >= amount)
        {
            ActualChakra -= amount;
            UpdateChakraBar();
        }
    }

    private void RegenerateChakra()
    {
        if (_characterLife.Life > 0f && ActualChakra < maxChakra)
        {
            ActualChakra += regenerationBySecond;
            UpdateChakraBar();
        }
    }

    public void RestoreChakra(float amount)
    {
        if (ActualChakra >= maxChakra)
        {
            return;
        }

        ActualChakra += amount;
        if (ActualChakra > maxChakra)
        {
            ActualChakra = maxChakra;
        }
        UIManager.Instance.UpdateCharacterChakra(ActualChakra,maxChakra);
    }

    public void RestoreChakraInitial()
    {
        ActualChakra = initialChakra;
        UpdateChakraBar();
    }

    private void UpdateChakraBar()
    {
        UIManager.Instance.UpdateCharacterChakra(ActualChakra, maxChakra);
    }
}