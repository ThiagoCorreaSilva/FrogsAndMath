using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    [SerializeField] protected float currentLife;
    [SerializeField] protected float maxLife;
    public bool death;

    protected virtual void Start()
    {
        currentLife = maxLife;
    }
    public virtual void Death()
    {
        death = true;
        gameObject.SetActive(false);
    }

    public void TakeDamage(float _dmg)
    {
        currentLife = Mathf.Max(currentLife - _dmg, 0f);

        if (currentLife == 0f)
            Death();
    }

    public void GainLife(float _life)
    {
        currentLife = Mathf.Min(currentLife + _life, maxLife);
    }
}
