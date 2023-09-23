using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField]
    CardScriptable cardScriptable;
    Collider2D collider;
    [SerializeField]
    CardInfo cardInfo;

    Image img;
    // Start is called before the first frame update
    void Awake()
    {
        this.collider = GetComponent<Collider2D>();
        img = GetComponent<Image>();
        if(cardScriptable!=null)
        {
            img.sprite = cardScriptable.sprite;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Rect GetBound()
    {   
        float width = 2*collider.bounds.extents.x;
        float height = 2*collider.bounds.extents.y;
        return new Rect(transform.position.x-width/2, transform.position.y-height/2, width, height);
    }


    public void SetCardScriptable(CardScriptable cardScript)
    {
        this.cardScriptable=cardScript;
        
        img.sprite = cardScriptable.sprite;
        if(cardScriptable.maskSprite != null)
        {
            cardScriptable.material.SetTexture("_MaskTex",cardScriptable.maskSprite.texture);
        }
        img.material = cardScriptable.material;

        cardInfo.Set(cardScriptable);
    }
}
