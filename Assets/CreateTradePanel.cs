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

    [SerializeField]
    TMP_Text infoText;
    void Start()
    {
        infoText.text = "";
        closeButton.onClick.AddListener(() => ClosePanel());
        collectionGrid.ApplyOnAll(InitTradeItemCollectionSide);
        postButton.onClick.AddListener(() => PostTrade());
        ClosePanel();
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
        Reset();
    }


    public void Reset()
    {
        tradeGrid.Reset();
        price.text = "";
        collectionGrid.ApplyOnAll(ResetCardCountValue);
    }
    public void ResetCardCountValue(CardGridScriptItem _item)
    {
        var tradeItem = (TradeGridItem)_item;
        foreach (var c in Client.instance.GetCollection())
        {
            if (c.script == _item.GetCardScriptable())
            {
                tradeItem.Init(c);
                tradeItem.UpdateText(false);
                break;
            }
        }
    }
    public void InitTradeItemCollectionSide(CardGridScriptItem _item)
    {
        var tradeItem = (TradeGridItem)_item;
        tradeItem.SetButtonListener((CardScriptable c) => Add(c), (CardScriptable c) => Remove(c));
        ResetCardCountValue(_item);
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
        infoText.text = "";
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
        infoText.text = "";
        TradeGridItem tradeItem = (TradeGridItem)tradeGrid.GetGridItem(c);
        if (tradeItem == null)
        {
            return;
        }

        bool needDestroy = !tradeItem.RemoveOne(true);
        if(needDestroy)
        {
            tradeGrid.RemoveItem(tradeItem.GetCardScriptable());
            Destroy(tradeItem.gameObject);
        }
        else
        {
            tradeItem.UpdateText(true); // 1 = dans la collection-en echangeTotale , 2 = en echange totale, 3 en echange dans cette echange précis.
        }

        TradeGridItem collectionItem = (TradeGridItem)collectionGrid.GetGridItem(c);

        collectionItem.RemoveOne(false);
        collectionItem.UpdateText(false);// 1 = dans la collection-en echangeTotale , 2 = en echange totale, 3 en echange dans cette echange précis.
    }

    public void PostTrade()
    {
        infoText.text = "";
        int p;
        if(!int.TryParse(price.text, out p))
        {
            infoText.text = "Tu dois indiquer un prix valide ...";
            return;
        }
        ClientSend.CreateTradeRequest(GetTradeCard(), p);
        ClosePanel();
    }

    public List<CardAndCount> GetTradeCard()
    {
        var result = new List<CardAndCount>();
        for(int i =0; i < tradeGrid.GetCardCount(); i++)
        {
            TradeGridItem tradeItem = (TradeGridItem)tradeGrid.GetCardGridScriptItem(i);
            int count = tradeItem.InThisTrade;
            result.Add(new CardAndCount(tradeItem.GetCardScriptable(), count));
        }
        return result;
    }
}

