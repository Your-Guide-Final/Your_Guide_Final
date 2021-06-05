using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("MovementInput")]
    public string Hinput;
    public string Vinput;

    [Header("Attaque Input")]
    public string attInput;

    [Header("Adrenaline Input")]
    public string switchInput;
    public string healInput;

    public float GetHorizontalInputAxis()
    {
        return Input.GetAxis(Hinput);
    }

    public float GetVerticalInputAxis()
    {
        return Input.GetAxis(Vinput);
    }

    public Vector3 GetDirectionInput()
    {
        Vector3 direction = new Vector3(GetHorizontalInputAxis(), 0, GetVerticalInputAxis());
        return direction;
    }

    public bool GetAttInputDown()
    {
        return Input.GetButtonDown(attInput);
    }

    public bool GetSwitchInputDown()
    {
        return Input.GetButtonDown(switchInput);
    }
}
