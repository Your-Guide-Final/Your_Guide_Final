using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiAttack : MonoBehaviour
{
    private EnemiControler eControler;

    public int degatValue = 1;

    public float forceDegatPlayer = 500;
    public float upwardForceModifier = 1;

    public Transform origineAtt;

    public float timeBumpPlayer;
    public float timeBumpReceptacle;

    public LayerMask colliderAttackLayer;

    bool AttackType;

    private void Awake()
    {
        eControler = transform.GetComponent<EnemiControler>();
        float random = Random.Range(0,1);
        if (random < 0.5)
        {
            AttackType = false;
        }
        else
        {
            AttackType = true;

        }
    }

    public void DegatCone(int degat, float rangeAtt, float effectiveRange, float knockBackForce)
    {
        AttackType = !AttackType;
        Collider[] colliderEntities = Physics.OverlapSphere(origineAtt.position, rangeAtt, colliderAttackLayer);

        foreach (Collider cible in colliderEntities)
        {
            Vector3 toCible = cible.transform.position - origineAtt.position;
            Vector3 knockBackDirection = toCible.normalized * knockBackForce;
            knockBackDirection.y = upwardForceModifier;

            float dotValue = Vector3.Dot(origineAtt.forward.normalized, toCible.normalized);

            if (dotValue >= effectiveRange)
            {
                ReceptacleControler rControler = cible.GetComponent<ReceptacleControler>();
                if (rControler)
                {
                    rControler.rLife.TakeDamage(degat);
                    rControler.rStatue.StartBump(knockBackDirection, timeBumpReceptacle);
                    //eControler.eFx.StartFxDegat(rControler.transform.position);
                }
                else
                {
                    PlayerControler pControler = cible.GetComponent<PlayerControler>();
                    if (pControler != null)
                    {
                        pControler.pStatue.Bump(knockBackDirection, timeBumpPlayer);
                        //eControler.eFx.StartFxDegat(pControler.transform.position);
                    }
                }
            }
        }
    }

    public void StartDamageCoroutine( float effectiveTimeBeforeDegat, int degatValue, float attRange, float effectiveRange, float bumpForce)
    {
        StartCoroutine(InflictDamage( effectiveTimeBeforeDegat,  degatValue, attRange, effectiveRange, bumpForce));
    }

    public IEnumerator InflictDamage( float effectiveTimeBeforeDegat, int degatValue, float attRange, float effectiveRange, float bumpForce)
    {
        yield return new WaitForSeconds(effectiveTimeBeforeDegat);
        DegatCone(degatValue, attRange, effectiveRange, bumpForce);
    }

    

}
