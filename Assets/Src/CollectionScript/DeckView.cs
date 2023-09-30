using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public struct CardAndCount: IComparable<CardAndCount>, IEquatable<CardAndCount>
{
    public CardAndCount(CardScriptable script, int count)
    {
        this.script = script;
        this.count = count;
    }
    [SerializeField]
    public CardScriptable script;
    [SerializeField]
    public int count;

    public int CompareTo(CardAndCount other)
    {
        if(other.script.serieNumber < script.serieNumber)
        {
            return 1;
        }
        else if(other.script.serieNumber > script.serieNumber)
        {
            return -1;
        }
        else
        {
            if(other.script.number < script.number)
            {
                return 1;
            }
            else if(other.script.number > script.number)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }

    public bool Equals(CardAndCount other)
    {
        return other.script.serieNumber == script.serieNumber && other.script.number == script.number;
    }
}

public class DeckView : MonoBehaviour
{
    List<DeckViewCard> deckViewCards;
    [SerializeField]
    GameObject viewCardPrefab;
    [SerializeField]
    CollectionManager collecManager;
    // Start is called before the first frame update
    void Start()
    {
        deckViewCards = new List<DeckViewCard>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DropCard(CardScriptable script)
    {
        int indexToRemove = -1;
        for(int index = 0; index < deckViewCards.Count; index++)
        {
            var dcv = deckViewCards[index];
            if(script.GetCardName() == deckViewCards[index].GetCardScriptable().GetCardName())
            {   
                var count = dcv.GetCount();
                if(count == 1)
                {
                    Destroy(dcv.gameObject);
                    indexToRemove = index;
                }
                else
                {
                    count -=1;
                    dcv.SetCount(count);
                }
                
                break;
            }
        }
        if(indexToRemove!=-1)
        {
            deckViewCards.RemoveAt(indexToRemove);
        }
    }
    public void AddCard(CardScriptable script)
    {
        collecManager.SetCardWatcherMode(script);
        bool found=false;
        for(int index=0 ; index<deckViewCards.Count; index++)
        {
            string name = deckViewCards[index].GetCardScriptable().GetCardName();
            if(name == script.GetCardName())
            {
                found = true;
                deckViewCards[index].AddOneToCount();
                break;
            }
        }
        if(!found)
        {
            var go = Instantiate(viewCardPrefab,this.transform);
            var dvc = go.GetComponent<DeckViewCard>();
            dvc.SetCardScriptable(script);
            dvc.SetCount(1);
            dvc.GetComponent<CollectionMouseHandler>().SetCollectionManager(collecManager);
            deckViewCards.Add(dvc);
            var btn = go.GetComponent<CollectionMouseHandler>();
            btn.SetCollectionManager(this.collecManager);
        }
    }
}
