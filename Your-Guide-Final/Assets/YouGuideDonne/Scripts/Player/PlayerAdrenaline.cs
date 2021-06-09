using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerAdrenaline : MonoBehaviour
{
    private PlayerControler pControler;

    [Header("Adrenaline")]
    [SerializeField] private float adrenalineStartValue;
    [SerializeField] private float adrenalineMaxValue;


    [Header("UI")]
    [SerializeField] private Slider jaugeFillSlider;
    [SerializeField] private Animator jaugeAnimator;
    [SerializeField] private string parameterJaugeName;
    [SerializeField] private float minValueToEnableHealAnime;
    [SerializeField] private float minValueToEnableAnimeGauge;
    [SerializeField] private float maxValueToDisableAnimeGauge;
    [SerializeField] private GameObject animeGaugeGameObject;
    [SerializeField] private float timeValueToLerpFill;

    [Header("Sign/Feedback")]
    [SerializeField] private SkinnedMeshRenderer hatMeshRenderer;
    private Material hatMaterial;
    
    [SerializeField] private Gradient EmissionColor;
    [SerializeField] private float maxEmissionIntensity;
    [SerializeField] private float minEmissionIntensity;
    private enum adrenalineEtat { Nothing, Switch,Heal}
    private adrenalineEtat etat;

    [Header("SFX")]
    [FMODUnity.EventRef]
    [SerializeField] private string eventSwitchAvaible;
    private bool alreadyFull;


    [HideInInspector]
    public float adrenalineValue;

    // Start is called before the first frame update
    private void Awake()
    {
        adrenalineValue = adrenalineStartValue;
        pControler = transform.GetComponent<PlayerControler>();
        etat = adrenalineEtat.Nothing;
        hatMaterial = hatMeshRenderer.material;
        hatMaterial.EnableKeyword("_EmissiveIntensity");
        alreadyFull = false;
    }

    public void SetJaugeFillValue()
    {
        float fillAmount = adrenalineValue / adrenalineMaxValue;
        jaugeFillSlider.value = Mathf.Lerp(jaugeFillSlider.value,fillAmount,timeValueToLerpFill*Time.deltaTime);
    }

    public bool IsAdrenalineMax()
    {
        bool isMax = adrenalineValue == adrenalineMaxValue;
        return isMax;
    }

    public void AddAdrenalineValue(float value)
    {
        adrenalineValue += value;
        adrenalineValue = Mathf.Clamp(adrenalineValue, 0, adrenalineMaxValue);
    }


    public void SetFeedBack()
    {
        bool canSwitch = pControler.pSwitch.IsInRange() && IsAdrenalineMax();
        bool canheal = pControler.pHeal.CanHeal();
       

        float pourcentageValue = adrenalineValue / adrenalineMaxValue;

        float IntensityValue = Mathf.Clamp(maxEmissionIntensity * pourcentageValue, minEmissionIntensity, maxEmissionIntensity);

        if (IsAdrenalineMax())
        {
            if (!alreadyFull)
            {
                alreadyFull = true;
                FMODUnity.RuntimeManager.PlayOneShot(eventSwitchAvaible, transform.position);
            }
        }
        else
        {
            alreadyFull = false;
        }

        if (canSwitch)
        {
            
            hatMaterial.SetColor("_BaseColor", EmissionColor.Evaluate(0));
            //Debug.Log("ColorChange");

            hatMaterial.SetColor("_EmissiveColor", EmissionColor.Evaluate(0) * IntensityValue);
            
        }
        else if (canheal)
        {
            hatMaterial.SetColor("_BaseColor", EmissionColor.Evaluate(1));
            //Debug.Log("ColorChange");

            hatMaterial.SetColor("_EmissiveColor", EmissionColor.Evaluate(1) * IntensityValue);
        }
        else
        {
            
            hatMaterial.SetColor("_BaseColor", EmissionColor.Evaluate(0.5f));
            hatMaterial.SetColor("_EmissiveColor", EmissionColor.Evaluate(0.5f) * IntensityValue);

            
        }

        if (pourcentageValue < minValueToEnableHealAnime)
        {
            jaugeAnimator.SetFloat(parameterJaugeName, 0);
        }
        else if (pourcentageValue < 1)
        {
            jaugeAnimator.SetFloat(parameterJaugeName, 1);
        }
        else
        {
            jaugeAnimator.SetFloat(parameterJaugeName, 2);
        }

        if (pourcentageValue < minValueToEnableAnimeGauge)
        {
            animeGaugeGameObject.SetActive(false);
        }
        else if (pourcentageValue < maxValueToDisableAnimeGauge)
        {
            animeGaugeGameObject.SetActive(true);
        }
        else
        {
            animeGaugeGameObject.SetActive(false);
        }

    }

    /*public void SetFeedBackColor()
    {
        
    }

    public void SetFeedBackIntensity()
    {
        //Material hatMateriel = hatMeshRenderer.material;

        

        
        //hatMaterial.SetFloat("_EmissiveIntensity", IntensityValue);
        //hatMaterial.SetFloat("_EmissiveExposureWeight", pourcentageValue);

    }*/

}
