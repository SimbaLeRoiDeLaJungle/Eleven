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

    public int Count { get { return count; } }
    public int InTrade { get { return in_trade; } }
    public int InThisTrade { get { return in_this_trade; } }

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
        if (count == 0)
            return;

        count--;
        in_trade++;
        in_this_trade++;
        if(count == 0)
        {
            addButton.interactable = false;
        }
    }
    public bool RemoveOne(bool inTradeGrid)
    {
        if (in_this_trade == 0)
            return false;

        count++;
        in_trade--;
        in_this_trade--;
        if(!addButton.interactable)
        {
            addButton.interactable = true;
        }
        if(in_this_trade == 0 && inTradeGrid)
        {
            return false;
        }
        return true;
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
        in_trade = cc.in_trade;
        count = cc.count - in_trade;
    }

    public void SetLockMode(bool lockMode, int _in_this_trade)
    {

        if(lockMode)
        {
            addButton.gameObject.SetActive(false);
            removeButton.gameObject.SetActive(false);
            infoCount.text = _in_this_trade.ToString();
        }
        else
        {
            addButton.gameObject.SetActive(true);
            removeButton.gameObject.SetActive(true);
        }

    }

    public void Reset()
    {
        addButton.gameObject.SetActive(true);

    }
}
