using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class DebugTool : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] string activeBarInput = "alt";
    [Space]
    [SerializeField] string resetSceneInput = "f1";
    [SerializeField] string loadMenuInput = "f10";
    [Space]
    [SerializeField] string healReceptacleInput = "f2";
    [SerializeField] string damageReceptacleInput = "f3";
    [SerializeField] string godModeOnOffInput = "f4";
    [SerializeField] string tpReceptacle = "f5";
    [Space]
    //[SerializeField] string adrenalineUpInput = "f5";
    [SerializeField] string adrenalineMaxInput = "f6";
    [SerializeField] string adrenalineInfiniInput = "f7";
    [Space]
    [SerializeField] string stopCombat = "f8";
    [Space]
    [SerializeField] List<string> tpInput;

    [Header("Scene")]
    [SerializeField] string menuName;

    [Header("PointToTP")]


    [Header("UI")]
    [SerializeField] GameObject debugUI;
    [SerializeField] Image godModeFont;
    [SerializeField] Image adrenalineInfiniFont;
    [SerializeField] Gradient colorOnOff;




    private PlayerControler pControler;
    private ReceptacleControler rControler;

    bool adrenalineInfini;

    private void Awake()
    {
        pControler = FindObjectOfType<PlayerControler>();
        rControler = FindObjectOfType<ReceptacleControler>();
        adrenalineInfini = false;
        if(debugUI!= null)
        {
            debugUI.SetActive(false);
        }
    }

    private void Update()
    {
        CheckInput();

        AdrenalineInfini();

    }

    

    public void CheckInput()
    {
        
        if (Input.GetKeyDown(activeBarInput))
        {
            debugUI.SetActive(!debugUI.activeSelf);
        }

        if (Input.GetKeyDown(resetSceneInput))
        {
            string activeScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(activeScene);
        }

        if (Input.GetKeyDown(loadMenuInput))
        {
            SceneManager.LoadScene(menuName);
        }

        if (Input.GetKeyDown(healReceptacleInput))
        {
            rControler.rLife.TakeDamage(-100);
        }

        if (Input.GetKeyDown(damageReceptacleInput))
        {
            rControler.rLife.TakeDamage(1);
        }

        if (Input.GetKeyDown(godModeOnOffInput))
        {
            bool actifGodModeValue = rControler.rLife.godMode;
            rControler.rLife.godMode = !actifGodModeValue;
            if (actifGodModeValue)
            {
                godModeFont.color = colorOnOff.Evaluate(0);
            }
            else
            {
                godModeFont.color = colorOnOff.Evaluate(1);
            }
        }

        if (Input.GetKeyDown(adrenalineUpInput))
        {
            pControler.pAdrenaline.AddAdrenalineValue(10);
        }

        if (Input.GetKeyDown(adrenalineMaxInput))
        {
            pControler.pAdrenaline.AddAdrenalineValue(100);
        }

        if (Input.GetKeyDown(adrenalineInfiniInput))
        {
            adrenalineInfini = !adrenalineInfini;

            if (adrenalineInfini)
            {
                adrenalineInfiniFont.color = colorOnOff.Evaluate(1);
            }
            else
            {
                adrenalineInfiniFont.color = colorOnOff.Evaluate(0);

            }
        }

    }

    public void AdrenalineInfini()
    {
        if (adrenalineInfini)
        {
            bool canUp = pControler.pAdrenaline.IsAdrenalineMax();

            if (!canUp)
            {
                pControler.pAdrenaline.AddAdrenalineValue(100);
            }
        }
    }
}
