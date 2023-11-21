using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighLightScript : MonoBehaviour
{
    [SerializeField]
    bool isBeingHighLight = false;

    [SerializeField]
    Shader shader;

    Renderer[] render;

    private void Awake()
    {
        render = GetComponentsInChildren<Renderer>();
        shader = Resources.Load<Shader>("Shader/HighLighShader");
        foreach (var ren in render)
        {
            if (!ren.gameObject.TryGetComponent<ParticleSystem>(out var par))
            {
                foreach (var mat in ren.materials)
                {
                    //var color = mat.color;
                    //var text = mat.mainTexture;
                    //Debug.Log(mat.name);

                    mat.shader = shader;
                    //mat.SetTexture
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        //This bit only run in the editor
#if UNITY_EDITOR
        UpdateShaderState();
#endif
    }


    public void HighLight()
    {
        isBeingHighLight = true;
        UpdateShaderState();
    }

    public void UnhighLight()
    {
        isBeingHighLight = false;
        UpdateShaderState();
    }

    private void UpdateShaderState()
    {
        foreach (var ren in render)
        {
            foreach (var mat in ren.materials)
            {
                float value = isBeingHighLight ? 1 : 0;
                mat.SetFloat("_HighLightValue", value);
            }
        }
    }
}
