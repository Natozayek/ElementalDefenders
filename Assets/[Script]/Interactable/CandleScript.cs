using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleScript : Interactable
{
    private bool isOn = true;
    ParticleSystem particle;
    Light light;
    private new void Awake()
    {
        base.Awake();
        particle = GetComponentInChildren<ParticleSystem>();
        light = GetComponentInChildren<Light>();
    }


    protected override void OnInteraction()
    {
        isOn = !isOn;

        if (isOn)
            particle.Play();
        else
            particle.Stop();

        light.gameObject.SetActive(isOn);
        //throw new System.NotImplementedException();
    }

}
