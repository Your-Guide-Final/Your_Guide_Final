using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SoulsControler : MonoBehaviour
{
    [Header("Timming")]
    [SerializeField] float timeToBeAbsorb;
    [SerializeField] float timeBetweenStaticAndDrop;
    [SerializeField] float timeBeforeDie;

    [Header("Vfx")]
    [SerializeField] VisualEffect staticSoulsVfx;
    [SerializeField] string stopStaticSoulsEvent;
    [SerializeField] VisualEffect dropSoulsVfx;
    [SerializeField] string startDropSoulsEvent;
    [SerializeField] string posTargetDropSoulsParameterName;
    [SerializeField] float upwardModifierPosTarget;

    [Header("BezierCurve")]
    [SerializeField] Vector3 bezierCurveParameter1;
    [SerializeField] string bezierCurveParameter1Name;
    [SerializeField] Vector3 bezierCurveParameter2;
    [SerializeField] string bezierCurveParameter2Name;

    ReceptacleControler rControler;
    Vector3 posTargetDrop;

    private void Start()
    {
        rControler = FindObjectOfType<ReceptacleControler>();
        dropSoulsVfx.SetVector3(bezierCurveParameter1Name, bezierCurveParameter1);
        dropSoulsVfx.SetVector3(bezierCurveParameter2Name, bezierCurveParameter2);

        if (rControler != null)
        {
            StartCoroutine(PlaySoulsAbsorb());
        }

    }


    public IEnumerator PlaySoulsAbsorb()
    {
        yield return new WaitForSeconds(timeToBeAbsorb);
        staticSoulsVfx.SendEvent(stopStaticSoulsEvent);

        yield return new WaitForSeconds(timeBetweenStaticAndDrop);
        posTargetDrop = rControler.transform.position + new Vector3(0, upwardModifierPosTarget, 0);
        dropSoulsVfx.SetVector3(posTargetDropSoulsParameterName, posTargetDrop);
        dropSoulsVfx.SendEvent(startDropSoulsEvent);

        yield return new WaitForSeconds(timeBeforeDie);
        Destroy(gameObject);
    }
}
