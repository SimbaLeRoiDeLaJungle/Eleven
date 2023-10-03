using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------- Card Grid Script -----------------------
// Permet de contenir des cartes dans une scrollview pour la collection , creation de deck echange etc ...
//-----------------------------------------------------------------
public class CardGridScript : MonoBehaviour
{
    List<CardAndCount> cardScripts; // contients les cartes dans la collection de l'utilisateurs
    
    [SerializeField]
    GameObject prefabCard; // les UI qui vont servir de base pour les cartes

    List<CardGridScriptItem> buttons = new List<CardGridScriptItem>(); // Contient les cartes qui sont afficher (En fonction des options de tri)

    int currentIndex; // pour pouvoir se déplacer

    public delegate void HandleClick(CardGridScriptItem ccb); // Pour implémenter ce qui se passe lorsque l'on clique sur un item, dans le but d'utiliser cette grille dans plusieurs contextes

    [SerializeField]
    HandleClick handleClick;

    public void SetHandleClick(HandleClick _handleClick) 
    {
        handleClick = _handleClick;
    }

    #region UnityMethods
    // Start is called before the first frame update
    void Start()
    {
        if(Client.instance != null)
        {
            cardScripts = Client.instance.GetCollection();
            Populate(new CollectionSortOptions());
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion UnityMethods

    public void SetCurrentCard(CardScriptable script)
    {
        currentIndex=0;
        for(int i = 0; i<buttons.Count; i++)
        {
            if(script == buttons[i].GetCardScriptable())
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

    public void Populate(CollectionSortOptions csoptions)
    {
        currentIndex = 0;
        Reset();

        if(csoptions.GetInCollection() && !csoptions.GetNotInCollection())
        {
            for (int index = 0; index < cardScripts.Count; index++)
            {
                bool goodType = csoptions.Get(cardScripts[index].script.GetCardType());
                bool goodRarety = csoptions.Get(cardScripts[index].script.rarety);
                bool containsText = cardScripts[index].script.cardRef.cardName.ToLower().Contains(csoptions.containText.ToLower());
                if (goodType && goodRarety && containsText)
                {
                    var go = Instantiate(prefabCard, this.transform);

                    CardGridScriptItem btn = go.GetComponent<CardGridScriptItem>();
                    buttons.Add(btn);
                    btn.Init(cardScripts[index].script);
                    CardGridItemClickHandler mouseHandler = go.GetComponent<CardGridItemClickHandler>();
                    mouseHandler.SetHandleClick(handleClick);
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
                    bool containsText = cards[j].cardRef.cardName.ToLower().Contains(csoptions.containText.ToLower());
                    if (goodType && goodRarety && containsText)
                    {
                        var go = Instantiate(prefabCard, this.transform);

                        CardGridScriptItem btn = go.GetComponent<CardGridScriptItem>();
                        buttons.Add(btn);
                        btn.Init(cards[j]);
                        btn.SetInCollection(Client.HaveCard(cards[j].serieNumber, cards[j].number));
                        CardGridItemClickHandler mouseHandler = go.GetComponent<CardGridItemClickHandler>();
                        mouseHandler.SetHandleClick(handleClick);

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
                    bool containsText = cards[j].cardRef.cardName.ToLower().Contains(csoptions.containText.ToLower());
                    if (goodType && goodRarety && containsText)
                    {
                        if (!Client.HaveCard(cards[j].serieNumber, cards[j].number))
                        {
                            var go = Instantiate(prefabCard, this.transform);

                            CardGridScriptItem btn = go.GetComponent<CardGridScriptItem>();
                            buttons.Add(btn);
                            btn.Init(cards[j]);
                            btn.SetInCollection(false);
                            CardGridItemClickHandler mouseHandler = go.GetComponent<CardGridItemClickHandler>();
                            mouseHandler.SetHandleClick(handleClick);
                        }
                    }

                }
            }
        }
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

}
