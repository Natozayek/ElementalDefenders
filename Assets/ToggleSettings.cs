using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSettings : MonoBehaviour
{
    [SerializeField] GameObject Settings, Instructions;


    public void Togglesettings()
    {
        Settings.SetActive(false);
    }
    public void ToggleInstructions()
    {
        Instructions.SetActive(false);
    }
    public void ToggleInstructionsON()
    {
        Instructions.SetActive(true);
    }

}
