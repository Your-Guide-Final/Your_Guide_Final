using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiProjectile : MonoBehaviour
{
    [HideInInspector] public int degatValue;
    public Rigidbody rigid;
    public float timeStunPlayer;

    [SerializeField] GameObject explosionImpact;

    private void OnCollisionEnter(Collision collision)
    {
        ReceptacleControler rControler = collision.transform.GetComponent<ReceptacleControler>();
        if (rControler != null)
        {
            rControler.rLife.TakeDamage(degatValue);
        }
        else
        {
            PlayerControler pControler = collision.transform.GetComponent<PlayerControler>();

            if (pControler != null)
            {
                pControler.pStatue.Stun(timeStunPlayer);
            }
            else
            {
                EnemiControler eControler = collision.transform.GetComponent<EnemiControler>();
                if (eControler != null)
                {
                    eControler.eLife.TakeDamage(degatValue);

                }
            }
        }

        GameObject explosion = Instantiate(explosionImpact, transform.position, transform.rotation);
        Destroy(explosion, 3f);
        Destroy(gameObject);
    }


    

    public void BumpRicochet(float force)
    {
        rigid.velocity.Set(0, 0, 0);
        Vector3 newDirection = transform.forward.normalized * -1;
        Vector3 newForce = newDirection * force;
    }
}
