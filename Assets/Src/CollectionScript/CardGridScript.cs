using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CardGridScript : MonoBehaviour
{

    [SerializeField]
    CollectionManager collecManager;

    List<CardAndCount> cardScripts;
    
    [SerializeField]
    GameObject prefabCard;

    [SerializeField]
    Vector2 padding;

    [SerializeField]
    Vector2 size;

    [SerializeField]
    Vector2 relativePosition;

    List<CollectionCardButton> buttons = new List<CollectionCardButton>();

    int currentIndex;
    // Start is called before the first frame update
    void Start()
    {
        if(Client.instance != null)
        {
            cardScripts = Client.instance.GetCollection();
            Populate();
            collecManager.SetCardWatcherMode(cardScripts[0].script);
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCurrentCard(CardScriptable script)
    {
        currentIndex=0;
        for(int i = 0; i<buttons.Count; i++)
        {
            if(script.GetCardName() == buttons[i].GetCardScriptable().GetCardName())
            {
                currentIndex = i;
                break;
            }
        }
    }

    public CardScriptable Previous()
    {
        currentIndex--;
        if(currentIndex<0)
            currentIndex = buttons.Count-1;

        return buttons[currentIndex].GetCardScriptable();
    }
    public CardScriptable Next()
    {
        currentIndex++;
        if(currentIndex>buttons.Count-1)
            currentIndex =0;
        return buttons[currentIndex].GetCardScriptable();
    }

    public void Populate()
    {
        Reset();
        for(int index=0; index<cardScripts.Count; index++)
        {
            var go = Instantiate(prefabCard,this.transform);

            CollectionCardButton btn = go.GetComponent<CollectionCardButton>();
            buttons.Add(btn);
            btn.Init(cardScripts[index].script);
            CollectionMouseHandler mouseHandler = go.GetComponent<CollectionMouseHandler>();
            mouseHandler.SetCollectionManager(this.collecManager);
        } 
    }


    public void PopulateByOptions(CollectionSortOptions csoptions)
    {
        currentIndex = 0;
        Reset();

        if(csoptions.GetInCollection() && !csoptions.GetNotInCollection())
        {
            for (int index = 0; index < cardScripts.Count; index++)
            {
                bool goodType = csoptions.Get(cardScripts[index].script.GetCardType());
                bool goodRarety = csoptions.Get(cardScripts[index].script.rarety);
                if (goodType && goodRarety)
                {
                    var go = Instantiate(prefabCard, this.transform);

                    CollectionCardButton btn = go.GetComponent<CollectionCardButton>();
                    buttons.Add(btn);
                    btn.Init(cardScripts[index].script);
                    CollectionMouseHandler mouseHandler = go.GetComponent<CollectionMouseHandler>();
                    mouseHandler.SetCollectionManager(this.collecManager);
                }

            }

        }
        else if(csoptions.GetInCollection() && csoptions.GetNotInCollection())
        {
            List<SerieScriptable> seriesDatas = SeriesData.instance.GetSeries();
            for (int i = 0; i < seriesDatas.Count; i++)
            {
                List<CardScriptable> cards = seriesDatas[i].GetCards();
                for (int j = 0; j < cards.Count; j++)
                {
                    bool goodType = csoptions.Get(cards[j].GetCardType());
                    bool goodRarety = csoptions.Get(cards[j].rarety);
                    if (goodType && goodRarety)
                    {
                        var go = Instantiate(prefabCard, this.transform);

                        CollectionCardButton btn = go.GetComponent<CollectionCardButton>();
                        buttons.Add(btn);
                        btn.Init(cards[j]);
                        btn.SetInCollection(Client.HaveCard(cards[j].serieNumber, cards[j].number));
                        CollectionMouseHandler mouseHandler = go.GetComponent<CollectionMouseHandler>();
                        mouseHandler.SetCollectionManager(this.collecManager);

                    }
                }
            }
        }
        else if (!csoptions.GetInCollection() && csoptions.GetNotInCollection())
        {
            List<SerieScriptable> seriesDatas = SeriesData.instance.GetSeries();
            for (int i = 0; i < seriesDatas.Count; i++)
            {
                List<CardScriptable> cards = seriesDatas[i].GetCards();
                for (int j = 0; j < cards.Count; j++)
                {
                    bool goodType = csoptions.Get(cards[j].GetCardType());
                    bool goodRarety = csoptions.Get(cards[j].rarety);
                    if (goodType && goodRarety)
                    {
                        if (!Client.HaveCard(cards[j].serieNumber, cards[j].number))
                        {
                            var go = Instantiate(prefabCard, this.transform);

                            CollectionCardButton btn = go.GetComponent<CollectionCardButton>();
                            buttons.Add(btn);
                            btn.Init(cards[j]);
                            btn.SetInCollection(false);
                            CollectionMouseHandler mouseHandler = go.GetComponent<CollectionMouseHandler>();
                            mouseHandler.SetCollectionManager(this.collecManager);
                        }
                    }

                }
            }
        }
        SortByText(csoptions.containText);
    }

    public void Reset()
    {
        buttons.Clear();
        foreach(Transform t in this.transform)
        {
            Destroy(t.gameObject);
        }
    }

    public CardScriptable GetCardInGrid(int index)
    {
        return buttons[index].GetCardScriptable();
    }

    public int GetCardCount()
    {
        return buttons.Count;
    }


    public void SortByText(string textToSearchLower)
    {        
        List<CollectionCardButton> result = new List<CollectionCardButton>();

        for (int i = 0; i < buttons.Count; i++)
        {
            if (!buttons[i].GetCardScriptable().cardRef.cardName.ToLower().Contains(textToSearchLower))
            {
                Destroy(buttons[i].gameObject);
            }
            else
            {
                result.Add(buttons[i]);
            }
        }
        buttons = result;
    }
}
