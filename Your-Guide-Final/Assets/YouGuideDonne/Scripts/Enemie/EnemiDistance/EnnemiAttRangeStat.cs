using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiAttRangeStat : MonoBehaviour
{
    private EnemiControler eControler;

     public float attaqueCooldown;
    [SerializeField] private Transform spawnPoint;

    [SerializeField] private GameObject projectile;
    [SerializeField] private float vitesseProjectile;

    public Vector3 targetOffset;

    public Transform arme;

    private void Awake()
    {
        eControler = transform.GetComponent<EnemiControler>();
    }


    public void SpawnProjectile()
    {
        GameObject newProjectile = Instantiate(projectile, spawnPoint.position, spawnPoint.rotation);
        EnemiProjectile eProjectile = newProjectile.transform.GetComponent<EnemiProjectile>();
        eProjectile.degatValue = eControler.eAttack.degatValue;


        Vector3 force = spawnPoint.forward.normalized * vitesseProjectile;
        eProjectile.rigid.AddForce(force,ForceMode.Impulse);

    }



}
