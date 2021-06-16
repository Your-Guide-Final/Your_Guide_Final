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
    [SerializeField] List<Transform> pointTp;

    [Header("UI")]
    [SerializeField] GameObject debugUI;
    [SerializeField] Image godModeFont;
    [SerializeField] Image adrenalineInfiniFont;
    [SerializeField] Gradient colorOnOff;


    private MenuManager menuManager;    

    private PlayerControler pControler;
    private ReceptacleControler rControler;

    bool adrenalineInfini;

    private void Awake()
    {
        menuManager = FindObjectOfType<MenuManager>();
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
            menuManager.StartLoadScene(activeScene);
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

        if (Input.GetKeyDown(tpReceptacle))
        {
            //pControler.pAdrenaline.AddAdrenalineValue(10);
            rControler.transform.position = pControler.transform.position + new Vector3(0, 0.2f, 0);
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

        if (Input.GetKeyDown(stopCombat))
        {
            CombatGestion[] combatGestions = FindObjectsOfType<CombatGestion>();
            foreach (var combatGestion in combatGestions)
            {
                if (combatGestion.onBattle)
                {
                    combatGestion.EndBattle();
                }
            }
        }

        for (int i = 0; i < tpInput.Count; i++)
        {
            if (Input.GetKeyDown(tpInput[i]))
            {
                if (pointTp[i] != null)
                {
                    pControler.pCharacterController.enabled = false;
                    pControler.transform.position = pointTp[i].position;
                    pControler.pCharacterController.enabled = true;
                    rControler.transform.position = pointTp[i].position + new Vector3(0, 0.2f, 0);
                }
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
