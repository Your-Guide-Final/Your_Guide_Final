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

    [SerializeField] private Image jaugeFillImage;

    [Header("Sign/Feedback")]
    [SerializeField] private SkinnedMeshRenderer hatMeshRenderer;
    private Material hatMaterial;
    
    [SerializeField] private Gradient EmissionColor;
    [SerializeField] private float maxEmissionIntensity;
    [SerializeField] private float minEmissionIntensity;
    private enum adrenalineEtat { Nothing, Switch,Heal}
    private adrenalineEtat etat;

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
    }

    public void SetJaugeFillValue()
    {
        float fillAmount = adrenalineValue / adrenalineMaxValue;
        jaugeFillImage.fillAmount = fillAmount;
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

    public bool canHeal()
    {



        return false;
    }


    public void SetFeedBack()
    {
        bool canSwitch = pControler.pSwitch.IsInRange() && IsAdrenalineMax();
        bool canheal = canHeal();
       

        float pourcentageValue = adrenalineValue / adrenalineMaxValue;

        float IntensityValue = Mathf.Clamp(maxEmissionIntensity * pourcentageValue, minEmissionIntensity, maxEmissionIntensity);

        if (canSwitch)
        {
            
            hatMaterial.SetColor("_BaseColor", EmissionColor.Evaluate(0));
            //Debug.Log("ColorChange");

            hatMaterial.SetColor("_EmissiveColor", EmissionColor.Evaluate(0) * IntensityValue);
        }
        else if (canheal)
        {

        }
        else
        {
            
            hatMaterial.SetColor("_BaseColor", EmissionColor.Evaluate(0.5f));
            hatMaterial.SetColor("_EmissiveColor", EmissionColor.Evaluate(0.5f) * IntensityValue);
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
