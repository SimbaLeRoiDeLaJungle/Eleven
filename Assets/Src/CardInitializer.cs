using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardInitializer : MonoBehaviour
{
    [SerializeField]
    Image image;

    [SerializeField]
    CardScriptable cardScriptable;
    
    public void SetCardScriptable(CardScriptable script)
    {
        cardScriptable = script;
    }

    public void UpdateRender(bool withMaterial)
    {
        image.sprite = cardScriptable.sprite;
        if (!withMaterial)
            return;

        image.material = new Material(cardScriptable.material);
        if(cardScriptable.maskSprite != null)
        {
            image.material.SetTexture("_MaskTex",cardScriptable.maskSprite.texture);
        }
    }


}
