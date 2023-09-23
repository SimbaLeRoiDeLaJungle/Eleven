using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoosterRenderInitializer : MonoBehaviour
{
    
    [SerializeField]
    Image image;

    [SerializeField]
    SerieScriptable serieScriptable;
    // Start is called before the first frame update
    void Start()
    {
        UpdateRender();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateRender()
    {
        image.sprite = serieScriptable.artSet;
        if(serieScriptable.maskArtSet != null)
        {
            image.material.SetTexture("_MaskTex",serieScriptable.maskArtSet.texture);
        }
    }
}
