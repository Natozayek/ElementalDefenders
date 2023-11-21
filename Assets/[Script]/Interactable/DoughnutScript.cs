using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoughnutScript : Interactable
{
    SpinScript spin;

    protected override void OnInteraction()
    {
        if(spin)
        {
            spin.rotationAngle += 1f;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        var loop = SaveManager.Load();
        for(int i = 0; i < loop; i++)
        {
            var nut = Instantiate<GameObject>(this.gameObject, transform.parent);
            var newPos = nut.transform.localPosition;
            newPos.y += 0.05f * (i + 1);
            nut.transform.localPosition = newPos;
            Destroy(nut.GetComponent<DoughnutScript>());
        }

        spin = GetComponent<SpinScript>();
    }
}
