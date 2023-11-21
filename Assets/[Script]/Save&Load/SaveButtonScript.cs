using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SaveButtonScript : Button
{
    protected override void Awake()
    {
        base.Awake();
        //onClick.AddListener(SaveManager.Save);
    }
}
