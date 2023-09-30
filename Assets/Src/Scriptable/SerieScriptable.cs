using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Serie", menuName = "Serie/new Serie")]
public class SerieScriptable : ScriptableObject
{
    public Sprite artSet;
    public Sprite maskArtSet;

    [SerializeField]
    List<CardScriptable> cards;

    Dictionary<CardRarety, int> raretyCount;

    [SerializeField]
    int faPull;
    [SerializeField]
    int altPull;
    [SerializeField]
    int pullOn;

    void Init()
    {
        raretyCount = new Dictionary<CardRarety, int>();
        raretyCount.Add(CardRarety.Common, 0);
        raretyCount.Add(CardRarety.UnCommon, 0);
        raretyCount.Add(CardRarety.Rare, 0);
        raretyCount.Add(CardRarety.FA, 0);
        raretyCount.Add(CardRarety.ALT,0);
        for(int i = 0 ; i< cards.Count; i++)
        {
            int value = raretyCount[cards[i].rarety] + 1;
            raretyCount[cards[i].rarety] = value;
        }
    }

    public List<CardScriptable> GetBooster()
    {
        if(raretyCount == null)
        {
            Init();
            Debug.Log("init");
        }
        List<CardScriptable> result = new List<CardScriptable>();
        for(int i =0; i < 10; i++)
        {
            CardRarety rarety;
            if(i<=8)
            {
                if(i%2 == 0)
                    rarety = CardRarety.Common;
                else
                    rarety = CardRarety.UnCommon;
            }
            else
            {
                int rand2 = Random.Range(0, pullOn);
                if(rand2 > pullOn - altPull)
                {
                    rarety = CardRarety.ALT;
                }
                else if(rand2 > pullOn - faPull - altPull)
                {
                    rarety = CardRarety.FA;
                }
                else
                {
                    rarety = CardRarety.Rare;
                }
                
            }
            int rand = Random.Range(0,raretyCount[rarety]);
            CardScriptable card = ProcessGet(rand, rarety);
            result.Add(card);
        }
        return result;
    }

    CardScriptable ProcessGet(int index, CardRarety rarety)
    {
        int j=0;
        for(int i = 0; i < cards.Count; i++)
        {
            if(rarety == cards[i].rarety)
            {
                if(j==index)
                    return cards[i];
                j+=1;
            }
        }
        return null;
    }

    public CardScriptable GetCard(int card_id)
    {
        foreach(CardScriptable card in cards) 
        {
            if(card.number == card_id)
            {
                return card;
            }
        }
        return null;
    }
}
