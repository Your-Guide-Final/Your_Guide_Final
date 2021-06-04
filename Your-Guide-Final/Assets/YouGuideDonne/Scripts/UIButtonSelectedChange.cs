using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIButtonSelectedChange : MonoBehaviour, IPointerEnterHandler, ISelectHandler
{
    /*bool selected;
    bool highlighted;*/

    [SerializeField] GameObject imageToActive;

    public void OnPointerEnter(PointerEventData eventData)
    {
        imageToActive.SetActive(true);
    }
    public void OnSelect(BaseEventData eventData)
    {
        imageToActive.SetActive(true);
    }

    private void Awake()
    {
        imageToActive.SetActive(false);
    }

    private void Update()
    {
        Button but = this.GetComponent<Button>();
        bool selected = EventSystem.current.currentSelectedGameObject.GetComponent<Button>() == but;
        //EventSystem.current.IsPointerOverGameObject()
        if (!selected)
        {
            imageToActive.SetActive(false);
        }
    }

}
