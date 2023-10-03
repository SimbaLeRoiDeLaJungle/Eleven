using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionCardButton : MonoBehaviour
{
    [SerializeField]
    CardScriptable cardScriptable;

    CardInitializer cardInitializer;
    
    [SerializeField]
    CardInfo cardInfo;
    // Start is called before the first frame update
    void Awake()
    {
        cardInitializer  = GetComponent<CardInitializer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(cardInfo.gameObject.GetComponent<RectTransform>().rect != GetComponent<RectTransform>().rect)
        {
            cardInfo.SetOnRect(GetComponent<RectTransform>().rect);
        }
    }

    public void Init(CardScriptable cardScriptable)
    {
        this.cardScriptable = cardScriptable;
        cardInitializer.SetCardScriptable(cardScriptable);
        cardInitializer.UpdateRender(false);
    }

    public Rect GetRect()
    {
        return GetComponent<RectTransform>().rect;
    }

    public CardScriptable GetCardScriptable()
    {
        return cardScriptable;
    }

    public void SetInCollection(bool inCollection)
    {
        if(!inCollection)
        {
            GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
        }
        else
        {
            GetComponent<Image>().color = new Color(1,1,1);
        }
    }
}
