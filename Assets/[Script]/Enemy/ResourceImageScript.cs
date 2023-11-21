using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceImageScript : MonoBehaviour
{
    public Animator imageAnim;
    public Image image;

    public Sprite NormalGem;
    public Sprite FireGem;
    public Sprite WaterGem;
    public Sprite EarthGem;

    private void Awake()
    {
        NormalGem = Resources.Load<Sprite>("ImgResources/NormalGem");
        FireGem = Resources.Load<Sprite>("ImgResources/FireGem");
        WaterGem = Resources.Load<Sprite>("ImgResources/WaterGem");
        EarthGem = Resources.Load<Sprite>("ImgResources/EarthGem");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        imageAnim.SetBool("DoStuff", false);
    }

    public void AnimStart(Transform TransformParent, int index)
    {
        gameObject.SetActive(true);
        int Resource = 0;
        if (index == 0) { Resource = 0; }
        else if (index == 1) { Resource = 1; }
        else if (index == 3 || index == 4 || index == 6) { Resource = 3; }
        else { Resource = 4; }



        switch (Resource)
        {
            case 0:
                image.sprite = FireGem;
                PlayAnimation(TransformParent);
                break;
            case 1:
                image.sprite = WaterGem;
                PlayAnimation(TransformParent);
                break;
            case 2:
                image.sprite = EarthGem;
                PlayAnimation(TransformParent);
                break;
            case 3:
                image.sprite = NormalGem;
                PlayAnimation(TransformParent);
                break;
            case 4:
                gameObject.SetActive(false);
                break;
        }


    }

    public void PlayAnimation(Transform TransformParent)
    {
        transform.rotation = Quaternion.Euler(TransformParent.rotation.x * -1.0f - 45, 90, 0);
        imageAnim.SetBool("DoStuff", true);
    }

}
