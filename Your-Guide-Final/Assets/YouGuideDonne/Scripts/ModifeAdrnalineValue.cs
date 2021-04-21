using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifeAdrnalineValue : MonoBehaviour
{
    PlayerControler pControler;
    public string input;

    private void Awake()
    {
        pControler = FindObjectOfType<PlayerControler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(input))
        {
            pControler.pAdrenaline.AddAdrenalineValue(10);
        }
    }
}
