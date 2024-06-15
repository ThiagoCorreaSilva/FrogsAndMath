using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    [SerializeField] protected float currentLife;
    [SerializeField] protected float maxLife;
    [SerializeField] private GameObject bloodEffect;
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

        if (bloodEffect != null)
        {
            GameObject _blood = Instantiate(bloodEffect, transform.position, Quaternion.identity);
            Destroy(_blood, 0.601f);
        }

        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        Invoke("AfterHit", 0.2f);

        if (currentLife == 0f)
            Death();
    }

    public void GainLife(float _life)
    {
        currentLife = Mathf.Min(currentLife + _life, maxLife);
    }

    private void AfterHit()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
