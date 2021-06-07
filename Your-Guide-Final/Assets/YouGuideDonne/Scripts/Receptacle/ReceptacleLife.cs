using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMOD;

public class ReceptacleLife : LifeGestion
{
    private ReceptacleControler rControler;
    [SerializeField] private Image lifeBarFill;

    [FMODUnity.ParamRef]
    [SerializeField] string lifeGlobalParaName;



    public override void Death()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        //Destroy(gameObject);
    }

    public void SetLifeBareValue()
    {
        if (lifeBarFill != null)
        {
            float fillValue = lifeValue / maxLifeValue;
            lifeBarFill.fillAmount = fillValue;

        }
    }

    public void SetLifeSoundParameter()
    {
        float parameterValue = lifeValue / maxLifeValue;
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName(lifeGlobalParaName, parameterValue);
    }

    public bool IsLifeMax()
    {
        bool lifeIsMax = lifeValue == maxLifeValue;
        return lifeIsMax;
    }

}
