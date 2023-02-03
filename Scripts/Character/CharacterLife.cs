using System;
using UnityEngine;

public class CharacterLife : LifeClass
{
    [SerializeField] private float RegenerationLife;
    public static Action EventCharacterDefeated;
    public bool Defeated { get; private set; }
    public bool CanBeHealed => Life < maxLife;

    private BoxCollider2D _boxCollider2D;

    private void Awake()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }


    protected override void Start()
    {
        base.Start();
        UpdateLifeBar(Life, maxLife);

        InvokeRepeating(nameof(RegenerateLife), 1, 1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(15);
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            RestoreLife(10);
        }
    }

    public void RestoreLife(float amount)
    {
        if (Defeated)
        {
            return;
        }

        if (CanBeHealed)
        {
            Life += amount;
            if (Life > maxLife)
            {
                Life = maxLife;
            }

            UpdateLifeBar(Life, maxLife);
        }
    }

    public void RegenerateLife()
    {
        if (Life > 0f && Life < maxLife)
        {
            Life += RegenerationLife;
            UpdateLifeBar(Life, maxLife);
        }
    }

    protected override void UpdateLifeBar(float actualLife, float newMaxLife)
    {
        UIManager.Instance.UpdateCharacterLife(actualLife, newMaxLife);
    }

    protected override void DefeatedCharacter()
    {
        _boxCollider2D.enabled = false;
        Defeated = true;
        EventCharacterDefeated?.Invoke();
    }

    public void ReviveCharacter()
    {
        _boxCollider2D.enabled = true;
        Defeated = false;
        Life = initialLife;
        UpdateLifeBar(Life, initialLife);
    }
}