using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatue : MonoBehaviour
{
    private PlayerControler pControler;

    [HideInInspector]
    public bool canMove;
    [HideInInspector]
    public bool onSwitch;
    [HideInInspector]
    public bool onRootMotion;


    public bool stun;

    public bool bump;

    public bool hyperArmor;

    private void Awake()
    {
        pControler = transform.GetComponent<PlayerControler>();
        stun = false;
        bump = false;
        hyperArmor = false;
        onRootMotion = false;
    }

    public void Stun(float timeStun)
    {
        if (!stun && !bump && !hyperArmor)
        {
            StartCoroutine(SetStun(timeStun));
        }
    }

    public IEnumerator SetStun(float timeStun)
    {
        stun = true;
        yield return new WaitForSeconds(timeStun);
        stun = false;
    }

    public void Bump(Vector3 bumpForce, float timeBump)
    {
        if(!stun && !bump && !hyperArmor)
        {
            StartCoroutine(SetBump(bumpForce, timeBump));
        }
    }

    public IEnumerator SetBump(Vector3 bumpForce, float timeBump)
    {
        bump = true;
        //pControler.rigid.AddForce(bumpForce);
        yield return new WaitForSeconds(timeBump);
        bump = false; 
    }

}
