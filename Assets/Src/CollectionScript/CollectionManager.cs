using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CollectionSortOptions
{
    public bool showBySort = true;
    public bool showByHeros = true;
    public bool showByArea = true;
    public bool alt = true;
    public bool fa = true;
    public bool rare = true;
    public bool unco = true;
    public bool co = true;
    public bool Get(CardRarety cardRarety)
    {
        if(cardRarety == CardRarety.ALT)
        {
            return alt;
        }
        else if(cardRarety == CardRarety.FA)
        {
            return fa;
        }
        else if(cardRarety == CardRarety.Rare)
        {
            return rare;
        }
        else if(cardRarety == CardRarety.UnCommon)
        {
            return unco;
        }
        else
        {
            return co;
        }
    }
    public bool Get(CardType cardType)
    {
        if(cardType == CardType.Area)
        {
            return showByArea;
        }
        else if(cardType == CardType.Heros)
        {
            return showByHeros;
        }
        else
        {
            return showBySort;
        }
    }

    public void Set(CardType cardType, bool value)
    {
        if(cardType == CardType.Area)
        {
            showByArea = value;
        }
        else if(cardType == CardType.Heros)
        {
            showByHeros = value;
        }
        else if(cardType == CardType.Sort)
        {
            showBySort = value;
        }
    }
    public void Switch(CardType cardType)
    {
        Debug.Log(cardType);
        if(cardType == CardType.Area)
        {
            showByArea = !showByArea;
        }
        else if(cardType == CardType.Heros)
        {
            showByHeros = !showByHeros;
        }
        else if(cardType == CardType.Sort)
        {
            showBySort = !showBySort;
        }
        
    }
    public void Switch(CardRarety cardRarety)
    {

        if(cardRarety == CardRarety.ALT)
        {
            alt = !alt;
        }
        else if(cardRarety == CardRarety.FA)
        {
            fa = !fa;
        }
        else if(cardRarety == CardRarety.Rare)
        {
            rare = !rare;
        }
        else if(cardRarety == CardRarety.UnCommon)
        {
            unco = !unco;
        }
        else if(cardRarety == CardRarety.Common)
        {
            co = !co;
        }
    }
}
public class CollectionManager : MonoBehaviour
{
    CollectionSortOptions csoptions = new CollectionSortOptions();

    public enum Mode
    {
        Collection,
        CardWatcher
    };
    [SerializeField]
    Button addButton;
    [SerializeField]
    Button removeButton;

    [SerializeField]
    Button leftButton;
    [SerializeField]
    Button rightButton;

    [SerializeField]
    CardGridScript cardGridScript;

    [SerializeField]
    CardWatcherControl cardWatcher;

    [SerializeField]
    DeckView deckView;

    Mode mode = Mode.Collection;

    CardScriptable currentCard;

    [SerializeField]
    Button sortSortButton;

    [SerializeField]
    Button herosSortButton;

    [SerializeField]
    Button areaSortButton;

    [SerializeField]
    Button altButton;

    [SerializeField]
    Button faButton;

    [SerializeField]
    Button rareButton;

    [SerializeField]
    Button uncoButton;

    [SerializeField]
    Button coButton;
    
    public void SetCollectionMode()
    {
        cardWatcher.gameObject.SetActive(false);
    }

    public void SetCardWatcherMode(CardScriptable cardInfo)
    {
        cardWatcher.gameObject.SetActive(true);
        currentCard = cardInfo;
        cardWatcher.SetCardScriptable(cardInfo);
        cardGridScript.SetCurrentCard(currentCard);
    }

    public void AddCardToDeck()
    {
        deckView.AddCard(currentCard);
    }

    public void RemoveCardToDeck()
    {
        deckView.DropCard(currentCard);
    }

    void Start()
    {
        addButton.onClick.AddListener(() => AddCardToDeck());
        removeButton.onClick.AddListener(() => RemoveCardToDeck());

        leftButton.onClick.AddListener(() => PreviousCard());
        rightButton.onClick.AddListener(() => NextCard());

        sortSortButton.onClick.AddListener(() => Sort(CardType.Sort));
        herosSortButton.onClick.AddListener(() => Sort(CardType.Heros));
        areaSortButton.onClick.AddListener(() => Sort(CardType.Area));

        altButton.onClick.AddListener( () => Sort(CardRarety.ALT));
        faButton.onClick.AddListener( () => Sort(CardRarety.FA));
        rareButton.onClick.AddListener( () => Sort(CardRarety.Rare));
        uncoButton.onClick.AddListener( () => Sort(CardRarety.UnCommon));
        coButton.onClick.AddListener( () => Sort(CardRarety.Common));
    }

    public void PreviousCard()
    {
        var cardScript = cardGridScript.Previous();
        cardWatcher.SetCardScriptable(cardScript);
    }
    public void NextCard()
    {
        var cardScript = cardGridScript.Next();
        cardWatcher.SetCardScriptable(cardScript);
    }

    public void Sort(CardType cardType)
    {
        csoptions.Switch(cardType);
        cardGridScript.PopulateByOptions(csoptions);
        UpdateSortButton(cardType);
    }

    public void Sort(CardRarety cardRarety)
    {
        csoptions.Switch(cardRarety);
        cardGridScript.PopulateByOptions(csoptions);
        UpdateSortButton(cardRarety);
        Debug.Log("pass");
    }
    public void InitCollectionBox()
    {
        cardGridScript.Populate();
    }

    public void UpdateSortButton(CardType cardType)
    {
        if(!csoptions.Get(cardType))
        {
            if(cardType == CardType.Sort)
            {
                sortSortButton.GetComponent<Image>().color = new Color(0.5f,0.5f,0.5f);
            }
            else if(cardType == CardType.Area)
            {
                areaSortButton.GetComponent<Image>().color = new Color(0.5f,0.5f,0.5f);
            }
            else if(cardType == CardType.Heros)
            {
                herosSortButton.GetComponent<Image>().color = new Color(0.5f,0.5f,0.5f);
            }
        }
        else
        {
            if(cardType == CardType.Sort)
            {
                sortSortButton.GetComponent<Image>().color = new Color(1f,1f,1f);
            }
            else if(cardType == CardType.Area)
            {
                areaSortButton.GetComponent<Image>().color = new Color(1f,1f,1f);
            }
            else if(cardType == CardType.Heros)
            {
                herosSortButton.GetComponent<Image>().color = new Color(1f,1f,1f);
            }
        }
    }
    public void UpdateSortButton(CardRarety cardRarety)
    {
        if(!csoptions.Get(cardRarety))
        {
            if(cardRarety == CardRarety.ALT)
            {
                altButton.GetComponent<Image>().color = new Color(0.5f,0.5f,0.5f);
            }
            else if(cardRarety == CardRarety.FA)
            {
                faButton.GetComponent<Image>().color = new Color(0.5f,0.5f,0.5f);
            }
            else if(cardRarety == CardRarety.Rare)
            {
                rareButton.GetComponent<Image>().color = new Color(0.5f,0.5f,0.5f);
            }
            else if(cardRarety == CardRarety.UnCommon)
            {
                uncoButton.GetComponent<Image>().color = new Color(0.5f,0.5f,0.5f);
            }
            else if(cardRarety == CardRarety.Common)
            {
                coButton.GetComponent<Image>().color = new Color(0.5f,0.5f,0.5f);
            }
        }
        else
        {
            if(cardRarety == CardRarety.ALT)
            {
                altButton.GetComponent<Image>().color = new Color(1f,1f,1f);
            }
            else if(cardRarety == CardRarety.FA)
            {
                faButton.GetComponent<Image>().color = new Color(1f,1f,1f);
            }
            else if(cardRarety == CardRarety.Rare)
            {
                rareButton.GetComponent<Image>().color = new Color(1f,1f,1f);
            }
            else if(cardRarety == CardRarety.UnCommon)
            {
                uncoButton.GetComponent<Image>().color = new Color(1f,1f,1f);
            }
            else if(cardRarety == CardRarety.Common)
            {
                coButton.GetComponent<Image>().color = new Color(1f,1f,1f);
            }
        }
    }
}
