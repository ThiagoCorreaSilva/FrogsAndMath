using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeController : MonoBehaviour
{
    [SerializeField] protected float currentLife;
    [SerializeField] protected float maxLife;
    [SerializeField] private GameObject bloodEffect;
    [SerializeField] private Slider lifeBar;
    public bool death;

    protected virtual void Start()
    {
        currentLife = maxLife;

        if (lifeBar != null)
            lifeBar.maxValue = maxLife;
    }
    protected virtual void Update()
    {
        if (lifeBar != null && !death)
            LifeBarUpdate();
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
        {
            Death();

            if (lifeBar != null)
                lifeBar.value = 0f;
        }
    }

    public void GainLife(float _life)
    {
        currentLife = Mathf.Min(currentLife + _life, maxLife);
    }

    private void LifeBarUpdate()
    {
        lifeBar.value = Mathf.Lerp(lifeBar.value, currentLife, 0.08f);
    }

    private void AfterHit()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
