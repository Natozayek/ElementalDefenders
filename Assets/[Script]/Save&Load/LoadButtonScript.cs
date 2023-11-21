using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoadButtonScript : Button
{
    protected override void Awake()
    {
        base.Awake();
        //onClick.AddListener(SaveManager.Load);
    }
}
