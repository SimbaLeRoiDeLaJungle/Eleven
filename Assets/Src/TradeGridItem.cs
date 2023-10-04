using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TradeGridItem : CardGridScriptItem
{
    [SerializeField]
    Button addButton;
    
    [SerializeField]
    Button removeButton;

    [SerializeField]
    TMP_Text infoCount;

    int count = 0;
    int in_trade = 0;
    int in_this_trade = 0;

    public delegate void AddDel(CardScriptable _cardScriptable);
    AddDel Add;
    public delegate void RemoveDel(CardScriptable _cardScriptable);
    RemoveDel Remove;
    
    public void SetButtonListener(AddDel _add, RemoveDel _remove)
    {
        Add = _add;
        Remove = _remove;
        addButton.onClick.AddListener(() => _add(GetCardScriptable()));
        removeButton.onClick.AddListener(() => _remove(GetCardScriptable()));
    }

    public void AddOne()
    {
        count--;
        in_trade++;
        in_this_trade++;
    }
    public void RemoveOne()
    {
        count++;
        in_trade--;
        in_this_trade--;
    }

    public void UpdateText(bool inTradeGrid)
    {
        if (inTradeGrid)
        {
            infoCount.text = in_this_trade.ToString();
        }
        else
        {
            infoCount.text = $"{count}/{in_trade}";
        }
    }

    public void Init(CardAndCount cc)
    {
        count = cc.count;
    }
}
