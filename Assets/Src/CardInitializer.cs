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

    public void UpdateRender()
    {
        image.sprite = cardScriptable.sprite;
        image.material = new Material(cardScriptable.material);
        if(cardScriptable.maskSprite != null)
        {
            image.material.SetTexture("_MaskTex",cardScriptable.maskSprite.texture);
        }
    }


}
