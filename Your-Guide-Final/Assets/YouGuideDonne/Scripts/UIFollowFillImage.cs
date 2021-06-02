using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFollowFillImage : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private RectTransform edgeImage;
    [SerializeField] private float decalValue;

    private void Update()
    {
        Vector2 edgeNewPos = new Vector2(fillImage.fillAmount * fillImage.rectTransform.rect.width - decalValue, edgeImage.localPosition.y);
        Debug.Log(edgeNewPos);
        edgeImage.localPosition = edgeNewPos;
    }
}
