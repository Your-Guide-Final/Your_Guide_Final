using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMOD;

public class ReceptacleLife : LifeGestion
{
    private ReceptacleControler rControler;
    [Header("UI")]
    [SerializeField] private Image lifeBarFill;
    [SerializeField] private float speedMoveLifebar;

    [FMODUnity.ParamRef]
    [SerializeField] string lifeGlobalParaName;



    public void Initialiaze()
    {
        rControler = transform.GetComponent<ReceptacleControler>();
    }

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
            lifeBarFill.fillAmount = Mathf.Lerp(lifeBarFill.fillAmount, fillValue, speedMoveLifebar);

        }
    }

    public void SetLifeSoundParameter()
    {
        
        float parameterValue = lifeValue / maxLifeValue;
        rControler.soundManager.SetLifeMusicValue(parameterValue);
    }

    public bool IsLifeMax()
    {
        bool lifeIsMax = lifeValue == maxLifeValue;
        return lifeIsMax;
    }

}
