using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiStatue : MonoBehaviour
{
    private EnemiControler eControler;

    public bool follow;
    public bool stun;
    public bool bump;

    public bool death;

    private void Awake()
    {
        eControler = transform.GetComponent<EnemiControler>();
        death = false;
        stun = false;
        bump = false;
    }

    public void Stun(float timeStun)
    {
        if (!stun && !bump)
        {
            StartCoroutine(SetStun(timeStun));
        }
    }

    public IEnumerator SetStun(float timeStun)
    {
        stun = true;
        eControler.eAnimator.enemiAnimator.SetTrigger(eControler.eAnimator.stunParameterName);
        eControler.eAnimator.enemiAnimator.SetBool(eControler.eAnimator.stunBoolParameterName, true);
        yield return new WaitForSeconds(timeStun);
        stun = false;
        eControler.eAnimator.enemiAnimator.SetBool(eControler.eAnimator.stunBoolParameterName, false);
    }

    public void Bump(Vector3 bumpForce, float timeBump)
    {
        if (!stun && !bump)
        {
            Debug.Log("enemiBump");
            StartCoroutine(SetBump(bumpForce, timeBump));
        }
    }

    public IEnumerator SetBump(Vector3 bumpForce, float timeBump)
    {
        bump = true;
        eControler.eAnimator.enemiAnimator.SetTrigger(eControler.eAnimator.bumpParameterName);
        eControler.eAnimator.enemiAnimator.SetBool(eControler.eAnimator.bumpBoolParameterName,true);

        eControler.rigid.AddForce(bumpForce,ForceMode.Impulse);
        yield return new WaitForSeconds(timeBump);
        bump = false;
        eControler.eAnimator.enemiAnimator.SetBool(eControler.eAnimator.bumpBoolParameterName,false);

    }

}
