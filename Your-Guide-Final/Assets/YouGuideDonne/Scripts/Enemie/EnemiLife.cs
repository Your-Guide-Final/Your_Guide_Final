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

    [Header("Death")]
    [SerializeField] GameObject particuleDeath;
    [SerializeField] GameObject soulsGameObject;
    [SerializeField] float timeToSpawnSouls;
    [FMODUnity.EventRef]
    [SerializeField] private string dropSoulsSfx;
    [SerializeField] float timeToStartDisolve;
    [SerializeField] float timeToDisolve;
    [SerializeField] List<SkinnedMeshRenderer> meshRenderers;
    [SerializeField] string disolveBodyParameterName;
    [SerializeField] string disolveMushParameterName;
    [SerializeField] float timeToDestroy;

    List<Material> materials;
    private void Awake()
    {
        lifeValue = initialLifeValue;
        Debug.Log(lifeValue);
        eControler = transform.GetComponent<EnemiControler>();
        materials = new List<Material>();

        foreach (var meshRenderer in meshRenderers)
        {
            Material[] meshMaterals = meshRenderer.materials;
            foreach (var mat in meshMaterals)
            {
                materials.Add(mat);
            }
        }
    }

    public override void Death()
    {
        if (!eControler.eStatue.death)
        {
            eControler.eStatue.death = true;
            eControler.eAnimator.enemiAnimator.SetBool(eControler.eAnimator.deathParameterName, true);
            if (combatGestion != null)
            {
                combatGestion.AnEnemiWasKill(transform);
            }

        }
    }

    public void SetLifeBareValue()
    {
        bool disableLifeBar = LifeIsMax() || lifeValue <= 0f;
        if (lifeFillImage != null)
        {
            if (disableLifeBar && lifeBarGameObject.activeSelf)
            {
                lifeBarGameObject.SetActive(false);
            }
            else if(!disableLifeBar && !lifeBarGameObject.activeSelf)
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


    public IEnumerator PlayDeath()
    {
        eControler.rigid.isKinematic = true;
        Collider collide = transform.GetComponent<Collider>();
        collide.enabled = false;
        float disolveTimer = 0f;
        Instantiate(particuleDeath, eControler.transform.position, eControler.transform.rotation);
        yield return new WaitForSeconds(timeToSpawnSouls);
        Instantiate(soulsGameObject, eControler.transform.position, eControler.transform.rotation);
        FMODUnity.RuntimeManager.PlayOneShot(dropSoulsSfx, transform.position);

        yield return new WaitForSeconds(timeToStartDisolve);

        while (disolveTimer<=timeToDisolve)
        {
            float effectiveTime = disolveTimer / timeToDisolve;
            //effectiveTime = 1 - effectiveTime;
            foreach (var mat in materials)
            {
                mat.SetFloat(disolveBodyParameterName, effectiveTime);
                mat.SetFloat(disolveMushParameterName, effectiveTime);
            }

            disolveTimer += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);

        }
        Destroy(gameObject, timeToDestroy);
    }

    public void StartDeath()
    {
        StartCoroutine(PlayDeath());
    }


}
