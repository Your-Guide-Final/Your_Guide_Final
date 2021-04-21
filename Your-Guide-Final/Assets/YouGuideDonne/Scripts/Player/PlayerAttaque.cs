using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerAttaque : MonoBehaviour
{
    private PlayerControler pControler;

    //public int degatValue;
    public LayerMask colliderAttackLayer;

    public Transform origineAttCone;
    public Transform origineAttImpact;

    public float attKnockBackUpModifier;

    public float timeBumpEnemi;

    [SerializeField] private float adrenalineGain;

    /*[Header("VFX")]

    public VisualEffect Attack1VFX;
    public VisualEffect Attack2VFX;
    public VisualEffect Attack3VFX;

    public string startEvent;
    public string degatEvent;

    public string degatPositionNameParameter;*/

    private void Awake()
    {
        pControler = GetComponent<PlayerControler>();
    }


    public void DegatCone(int degat, float rangeAtt, float effectiveRange, float knockBackForce, PlayerFX.typeOfAttack type, bool getAdrenaline)
    {
        Collider[] colliderEntities = Physics.OverlapSphere(origineAttCone.position, rangeAtt, colliderAttackLayer);

        foreach (Collider cible in colliderEntities)
        {
            Vector3 toCible = cible.transform.position - origineAttCone.position;
            Vector3 knockBackDirection = toCible.normalized * knockBackForce;
            knockBackDirection.y = attKnockBackUpModifier;

            float dotValue = Vector3.Dot(origineAttCone.forward.normalized, toCible.normalized);

            if(dotValue>= effectiveRange)
            {
                EnemiControler eControler = cible.GetComponent<EnemiControler>();
                if (eControler)
                {
                    Debug.Log("enemiHit");
                    
                    eControler.eLife.TakeDamage(degat);
                    //pControler.pFX.startFXDegat(type,cible.transform.position);
                    eControler.eStatue.Bump(knockBackDirection, timeBumpEnemi);
                    if (getAdrenaline)
                    {
                        pControler.pAdrenaline.AddAdrenalineValue(adrenalineGain);
                    }
                }
                else
                {
                    EnemiProjectile eProjectile = cible.GetComponent<EnemiProjectile>();
                    if (eProjectile != null)
                    {
                        //pControler.pFX.startFXDegat(type, cible.transform.position);
                        eProjectile.BumpRicochet(knockBackForce);

                        if (getAdrenaline)
                        {
                            pControler.pAdrenaline.AddAdrenalineValue(adrenalineGain);
                        }
                    }
                }
            }
        }
    }


    public void DegatSphere(int degat, float rangeAtt, float knockBackForce, PlayerFX.typeOfAttack type, bool getAdrenaline)
    {
        Collider[] colliderEntities = Physics.OverlapSphere(origineAttCone.position, rangeAtt, colliderAttackLayer);

        foreach (Collider cible in colliderEntities)
        {
            Vector3 toCible = cible.transform.position - origineAttCone.position;
            Vector3 knockBackDirection = toCible.normalized * knockBackForce;
            knockBackDirection.y = attKnockBackUpModifier;

            EnemiControler eControler = cible.GetComponent<EnemiControler>();
            if (eControler)
            {
                eControler.eLife.TakeDamage(degat);
                //pControler.pFX.startFXDegat(type, cible.transform.position);
                eControler.eStatue.Bump(knockBackDirection, timeBumpEnemi);
                if (getAdrenaline)
                {
                    pControler.pAdrenaline.AddAdrenalineValue(adrenalineGain);
                }
            }
            else
            {
                EnemiProjectile eProjectile = cible.GetComponent<EnemiProjectile>();
                if (eProjectile != null)
                {
                    //pControler.pFX.startFXDegat(type, cible.transform.position);
                    eProjectile.BumpRicochet(knockBackForce);
                    if (getAdrenaline)
                    {
                        pControler.pAdrenaline.AddAdrenalineValue(adrenalineGain);
                    }
                }
            }
        }
    }


    public void StartDamageCoroutine( float effectiveTimeBeforeDegat, bool useConeDetection, int degatValue, float attRange, float effectiveRange, float bumpForce, PlayerFX.typeOfAttack type, bool getAdrenaline)
    {
        StartCoroutine(InflictDamage( effectiveTimeBeforeDegat, useConeDetection, degatValue, attRange, effectiveRange, bumpForce,type, getAdrenaline));
    }

    public IEnumerator InflictDamage( float effectiveTimeBeforeDegat, bool useConeDetection, int degatValue, float attRange, float effectiveRange, float bumpForce, PlayerFX.typeOfAttack type, bool getAdrenaline)
    {
        yield return new WaitForSeconds(effectiveTimeBeforeDegat);
        if (useConeDetection)
        {
            DegatCone(degatValue, attRange, effectiveRange, bumpForce,type, getAdrenaline);

        }
        else
        {
            DegatSphere(degatValue, attRange, bumpForce,type, getAdrenaline);
        }
    }

}
