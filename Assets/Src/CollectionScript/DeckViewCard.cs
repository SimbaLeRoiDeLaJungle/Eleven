using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeckViewCard : MonoBehaviour
{
    CardScriptable cardScriptable;
    [SerializeField]
    TMP_Text countText;
    int count;
    CardGridScriptItem ccb;
    // Start is called before the first frame update
    void Awake()
    {
        ccb = GetComponent<CardGridScriptItem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCardScriptable(CardScriptable script)
    {
        this.cardScriptable = script;
        ccb.Init(script);
    }

    public void SetCount(int count)
    {
        countText.text = count.ToString();
        this.count = count;
    }

    public void AddOneToCount()
    {
        
        count += 1;
        countText.text = this.count.ToString();
    }

    public int GetCount()
    {
        return count;
    }
    
    public CardScriptable GetCardScriptable()
    {
        return cardScriptable;
    }

}
