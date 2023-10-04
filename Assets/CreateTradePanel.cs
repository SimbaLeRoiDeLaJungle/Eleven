using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateTradePanel : MonoBehaviour
{
    [SerializeField]
    TMP_InputField price;

    [SerializeField]
    Button postButton;

    [SerializeField]
    Button closeButton;

    [SerializeField]
    CardGridScript collectionGrid;

    [SerializeField]
    CardGridScript tradeGrid;

    void Start()
    {
        closeButton.onClick.AddListener(() => ClosePanel());
        collectionGrid.ApplyOnAll(InitTradeItemCollectionSide);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ShowPanel()
    {
        gameObject.SetActive(true);
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }



    public void InitTradeItemCollectionSide(CardGridScriptItem _item)
    {
        var tradeItem = (TradeGridItem)_item;
        tradeItem.SetButtonListener((CardScriptable c) => Add(c), (CardScriptable c) => Remove(c));
        foreach(var c in Client.instance.GetCollection())
        {
            if(c.script == _item.GetCardScriptable())
            {
                tradeItem.Init(c);
                tradeItem.UpdateText(false);
                break;
            }
        }
    }   
    public void InitTradeItemTradeSide(CardGridScriptItem _item)
    {
        var tradeItem = (TradeGridItem)_item;
        tradeItem.SetButtonListener((CardScriptable c) => Add(c), (CardScriptable c) => Remove(c));
        foreach(var c in Client.instance.GetCollection())
        {
            if(c.script == _item.GetCardScriptable())
            {
                tradeItem.Init(c);
                tradeItem.UpdateText(true);
                break;
            }
        }
    }

    public void Add(CardScriptable c)
    {
        TradeGridItem tradeItem = (TradeGridItem)tradeGrid.GetGridItem(c);
        if(tradeItem == null)
        {
            tradeGrid.AddAtTheEnd(c,false);
            tradeItem = (TradeGridItem)tradeGrid.GetGridItem(c);
            InitTradeItemTradeSide(tradeItem);
        }

        tradeItem.AddOne();
        tradeItem.UpdateText(true); // 1 = dans la collection-en echangeTotale , 2 = en echange totale, 3 en echange dans cette echange précis.

        TradeGridItem collectionItem = (TradeGridItem)collectionGrid.GetGridItem(c);
        collectionItem.AddOne();
        collectionItem.UpdateText(false);// 1 = dans la collection-en echangeTotale , 2 = en echange totale, 3 en echange dans cette echange précis.
    }
    public void Remove(CardScriptable c)
    {
        TradeGridItem tradeItem = (TradeGridItem)tradeGrid.GetGridItem(c);
        if (tradeItem == null)
        {
            return;
        }

        tradeItem.RemoveOne();
        tradeItem.UpdateText(true); // 1 = dans la collection-en echangeTotale , 2 = en echange totale, 3 en echange dans cette echange précis.
        
        TradeGridItem collectionItem = (TradeGridItem)collectionGrid.GetGridItem(c);
        collectionItem.RemoveOne();
        collectionItem.UpdateText(false);// 1 = dans la collection-en echangeTotale , 2 = en echange totale, 3 en echange dans cette echange précis.
    }
}
