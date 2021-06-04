using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemiLife : LifeGestion
{
    private EnemiControler eControler;
    [SerializeField] private Image lifeFillImage;
    [SerializeField] private CombatGestion.ennemiType typeRespawn;
    
    //[HideInInspector]
    public CombatGestion combatGestion;

    private void Awake()
    {
        lifeValue = initialLifeValue;
        Debug.Log(lifeValue);
        eControler = transform.GetComponent<EnemiControler>();
    }

    public override void Death()
    {
        eControler.eStatue.death = true;
        eControler.eAnimator.enemiAnimator.SetBool(eControler.eAnimator.deathParameterName, true);
        if (combatGestion != null)
        {
            combatGestion.Respawn(typeRespawn);
        }
    }

    public void SetLifeBareValue()
    {
        if (lifeFillImage != null)
        {
            float fillValue = lifeValue/maxLifeValue;
            //Debug.Log(fillValue);
            lifeFillImage.fillAmount = fillValue;

        }
    }

    public override void TakeDamage(int DamageValue)
    {
        base.TakeDamage(DamageValue);
        eControler.eFx.PlayDegatFx();
    }


}
