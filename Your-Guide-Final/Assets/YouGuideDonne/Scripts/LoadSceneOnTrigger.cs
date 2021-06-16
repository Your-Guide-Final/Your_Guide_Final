using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneOnTrigger : MonoBehaviour
{
    [SerializeField] string sceneName;
    [SerializeField] bool finalTransition;

    MenuManager menuManager;

    private void Awake()
    {
        menuManager = FindObjectOfType<MenuManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerControler pControler = other.GetComponent<PlayerControler>();

        if(pControler != null)
        {
            if (finalTransition)
            {
                menuManager.StartLoadSceneFinal(sceneName);
            }
            else
            {
                menuManager.StartLoadScene(sceneName);
            }
        }
    }
}
