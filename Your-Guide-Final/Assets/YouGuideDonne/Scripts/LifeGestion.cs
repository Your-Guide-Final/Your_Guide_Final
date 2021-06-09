using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeGestion : MonoBehaviour
{

    public float maxLifeValue;
    [SerializeField] protected float initialLifeValue;
    public bool godMode = false;
    protected float lifeValue;

    private void Awake()
    {
        lifeValue = initialLifeValue;
    }

    public virtual void TakeDamage(int DamageValue)
    {
        lifeValue -= DamageValue;
        lifeValue = Mathf.Clamp(lifeValue, 0, maxLifeValue);

        Debug.Log(lifeValue, gameObject);

        if (lifeValue == 0 && !godMode)
        {
            Death();
        }
    }

    public virtual void Death()
    {

    }

    public void changeGodMode()
    {
        godMode = !godMode;
    }

}
