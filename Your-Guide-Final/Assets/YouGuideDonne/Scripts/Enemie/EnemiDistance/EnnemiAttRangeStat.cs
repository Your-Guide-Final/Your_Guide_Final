using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class EnnemiAttRangeStat : MonoBehaviour
{
    private EnemiControler eControler;

    [Header("Attaque")]
    public float attaqueCooldown;
    [SerializeField] private Transform spawnPoint;

    [SerializeField] private GameObject projectile;
    [SerializeField] private float vitesseProjectile;

    [Header("Fx")]
    [SerializeField] private VisualEffect aimFx;
    [SerializeField] private string startAim;
    [SerializeField] private string stopAim;
    [SerializeField] private string endPosPropertyNameAim;

    [Header("Charge")]
    public float timeCharge;
    public string chargeAnimatorParameter;

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

    public IEnumerator SpawnProjectileWithDelay(float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);
        SpawnProjectile();
    }

    public void StartSpawnCoroutine(float timeDelay)
    {
        StartCoroutine(SpawnProjectileWithDelay(timeDelay));
    }

    public void StartCharge()
    {
        aimFx.SendEvent(startAim);
        aimFx.SetVector3(endPosPropertyNameAim, eControler.eMovement.currentTarget.position);
    }

    public void GetAim()
    {
        Transform target = eControler.eMovement.currentTarget;
        arme.LookAt(eControler.eMovement.currentTarget.position + targetOffset);
        aimFx.SetVector3(endPosPropertyNameAim, target.position);
    }

    public void StopCharge()
    {
        aimFx.SendEvent(stopAim);

    }

}
