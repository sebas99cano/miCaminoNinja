using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeClass : MonoBehaviour
{
    [SerializeField] protected float initialLife;

    [SerializeField] protected float maxLife;

    public float Life { get; protected set; }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Life = initialLife;
    }

    public void TakeDamage(float amount)
    {
        if (amount <= 0)
        {
            return;
        }

        if (Life > 0f)
        {
            Life -= amount;
            UpdateLifeBar(Life,maxLife);
            if (Life <= 0f)
            {
                Life = 0f;
                UpdateLifeBar(Life,maxLife);
                DefeatedCharacter();
            }
        }
        
    }

    protected virtual void UpdateLifeBar(float actualLife, float newMaxLife)
    {
        
    }

    protected virtual void DefeatedCharacter()
    {
        
    }

}