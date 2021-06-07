using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    TextMeshProUGUI texte;


    private void Awake()
    {
        texte = transform.GetComponent<TextMeshProUGUI>();
    }
    // Update is called once per frame
    void Update()
    {
        float fps = (1 / Time.smoothDeltaTime);
        fps = Mathf.RoundToInt(fps);
        texte.text ="Fps :" + fps;
    }
}
