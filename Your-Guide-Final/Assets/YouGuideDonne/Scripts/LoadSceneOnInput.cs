using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnInput : MonoBehaviour
{
    public string input;
    public string sceneName;

    MenuManager menu;
    // Start is called before the first frame update
    void Awake()
    {
        menu = FindObjectOfType<MenuManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(input))
        {
            menu.StartLoadScene(sceneName);
        }
    }

    

    
}
