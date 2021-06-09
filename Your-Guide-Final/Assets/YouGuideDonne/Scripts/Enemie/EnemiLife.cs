using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemiLife : LifeGestion
{
    private EnemiControler eControler;
    [SerializeField] private Image lifeFillImage;
    [SerializeField] private GameObject lifeBarGameObject;
    [SerializeField] private float speedMoveLifebar;
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
            combatGestion.AnEnemiWasKill();
        }
    }

    public void SetLifeBareValue()
    {
        if (lifeFillImage != null)
        {
            if (LifeIsMax() && lifeBarGameObject.activeSelf)
            {
                lifeBarGameObject.SetActive(false);
            }
            else if(!LifeIsMax() && !lifeBarGameObject.activeSelf)
            {
                lifeBarGameObject.SetActive(true);
            }
            float fillValue = lifeValue/maxLifeValue;
            //Debug.Log(fillValue);
            lifeFillImage.fillAmount = Mathf.Lerp(lifeFillImage.fillAmount, fillValue, speedMoveLifebar * Time.deltaTime);

        }
    }

    public bool LifeIsMax()
    {
        bool lifeMax = lifeValue == maxLifeValue;
        return lifeMax;
    }

    public override void TakeDamage(int DamageValue)
    {
        base.TakeDamage(DamageValue);
        eControler.eFx.PlayDegatFx();
    }


}
