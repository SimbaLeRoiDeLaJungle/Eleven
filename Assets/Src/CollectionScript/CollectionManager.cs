using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public bool inCollection = true;
    public bool notInCollection = false;
    public string containText = "";
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

    public bool GetInCollection()
    {
        return inCollection;
    }
    public bool GetNotInCollection()
    {
        return notInCollection;
    }
    public void SwitchInCollection()
    {
        inCollection =!inCollection;
    }
    public void SwitchNotInCollection()
    {
        notInCollection = !notInCollection;
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
    public static CollectionManager instance;

    CollectionSortOptions csoptions = new CollectionSortOptions();

    public enum Mode
    {
        Collection,
        CardWatcher
    };

    [SerializeField]
    Button leftButton;
    [SerializeField]
    Button rightButton;

    [SerializeField]
    CardGridScript cardGridScript;

    [SerializeField]
    CardWatcherControl cardWatcher;

    Mode mode = Mode.Collection;

    CardScriptable currentCard;

    #region SortButton
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

    [SerializeField]
    Button inCollectionButton;

    [SerializeField]
    Button notInCollectionButton;

    #endregion SortButton

    [SerializeField]
    Animator sortPanelAnimator;

    [SerializeField]
    TMP_Text cardCount;

    [SerializeField]
    CollectionUIVersionSwitcher collectionUIVersionSwitcher;

    [SerializeField]
    TMP_InputField searchInputField;
    [SerializeField]
    Button searchButton;

    public void SetCollectionMode()
    {
        cardWatcher.gameObject.SetActive(false);
    }

    public void SetCardWatcherMode(CardScriptable cardScriptable)
    {
        cardWatcher.gameObject.SetActive(true);
        currentCard = cardScriptable;
        cardWatcher.SetCardScriptable(cardScriptable);
        cardGridScript.SetCurrentCard(currentCard);
        cardCount.text = Client.GetCardCount(currentCard.serieNumber, currentCard.number).ToString();
        collectionUIVersionSwitcher.Clear();
        List<CardScriptable> sameVersions = SeriesData.instance.FindRarityByRef(cardScriptable);
        collectionUIVersionSwitcher.Set(sameVersions);
    }

    public void OpenCloseSortPanel()
    {
        
        if(sortPanelAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.CollectionSortPanelBaseAnim"))
        {
            sortPanelAnimator.SetTrigger("hide");
        }
        else if(sortPanelAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.CollectionSortPanelBaseHideAnim"))
        {
            sortPanelAnimator.SetTrigger("show");
        }
    }

    #region UnityMethods
    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {

        leftButton.onClick.AddListener(() => PreviousCard());
        rightButton.onClick.AddListener(() => NextCard());

        inCollectionButton.onClick.AddListener(() => Sort("inCollection"));
        notInCollectionButton.onClick.AddListener(() => Sort("notInCollection"));

        UpdateSortButton("notInCollection");

        sortSortButton.onClick.AddListener(() => Sort(CardType.Sort));
        herosSortButton.onClick.AddListener(() => Sort(CardType.Heros));
        areaSortButton.onClick.AddListener(() => Sort(CardType.Area));

        altButton.onClick.AddListener( () => Sort(CardRarety.ALT));
        faButton.onClick.AddListener( () => Sort(CardRarety.FA));
        rareButton.onClick.AddListener( () => Sort(CardRarety.Rare));
        uncoButton.onClick.AddListener( () => Sort(CardRarety.UnCommon));
        coButton.onClick.AddListener( () => Sort(CardRarety.Common));
    }
    #endregion UnityMethods

    public void PreviousCard()
    {
        var cardScript = cardGridScript.Previous();
        cardWatcher.SetCardScriptable(cardScript);
        cardCount.text = Client.GetCardCount(cardScript.serieNumber, cardScript.number).ToString();
        collectionUIVersionSwitcher.Clear();
        List<CardScriptable> sameVersions = SeriesData.instance.FindRarityByRef(cardScript);
        collectionUIVersionSwitcher.Set(sameVersions);
    }
    public void NextCard()
    {
        var cardScript = cardGridScript.Next();
        cardWatcher.SetCardScriptable(cardScript);
        cardCount.text = Client.GetCardCount(cardScript.serieNumber, cardScript.number).ToString();
        collectionUIVersionSwitcher.Clear();
        List<CardScriptable> sameVersions = SeriesData.instance.FindRarityByRef(cardScript);
        collectionUIVersionSwitcher.Set(sameVersions);
    }

    #region SortMethods
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
    }

    public void Sort(string name)
    {
        if(name == "inCollection")
        {
            csoptions.SwitchInCollection();
            cardGridScript.PopulateByOptions(csoptions);
            UpdateSortButton(name);
        }
        else if(name == "notInCollection")
        {
            csoptions.SwitchNotInCollection();
            cardGridScript.PopulateByOptions(csoptions);
            UpdateSortButton(name);
        }
    }
    public void UpdateSortButton(string name)
    {
        if (name == "inCollection")
        {
            if (!csoptions.GetInCollection())
            {
                inCollectionButton.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
            }
            else
            {
                inCollectionButton.GetComponent<Image>().color = new Color(1f, 1f, 1f);
            }
        }
        else if (name == "notInCollection")
        {
            if (!csoptions.GetNotInCollection())
            {
                notInCollectionButton.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
            }
            else
            {
                notInCollectionButton.GetComponent<Image>().color = new Color(1f, 1f, 1f);
            }
        }
    }
    public void UpdateSortButton(CardType cardType)
    {
        if (!csoptions.Get(cardType))
        {
            if (cardType == CardType.Sort)
            {
                sortSortButton.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
            }
            else if (cardType == CardType.Area)
            {
                areaSortButton.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
            }
            else if (cardType == CardType.Heros)
            {
                herosSortButton.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
            }
        }
        else
        {
            if (cardType == CardType.Sort)
            {
                sortSortButton.GetComponent<Image>().color = new Color(1f, 1f, 1f);
            }
            else if (cardType == CardType.Area)
            {
                areaSortButton.GetComponent<Image>().color = new Color(1f, 1f, 1f);
            }
            else if (cardType == CardType.Heros)
            {
                herosSortButton.GetComponent<Image>().color = new Color(1f, 1f, 1f);
            }
        }
    }
    public void UpdateSortButton(CardRarety cardRarety)
    {
        if (!csoptions.Get(cardRarety))
        {
            if (cardRarety == CardRarety.ALT)
            {
                altButton.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
            }
            else if (cardRarety == CardRarety.FA)
            {
                faButton.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
            }
            else if (cardRarety == CardRarety.Rare)
            {
                rareButton.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
            }
            else if (cardRarety == CardRarety.UnCommon)
            {
                uncoButton.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
            }
            else if (cardRarety == CardRarety.Common)
            {
                coButton.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
            }
        }
        else
        {
            if (cardRarety == CardRarety.ALT)
            {
                altButton.GetComponent<Image>().color = new Color(1f, 1f, 1f);
            }
            else if (cardRarety == CardRarety.FA)
            {
                faButton.GetComponent<Image>().color = new Color(1f, 1f, 1f);
            }
            else if (cardRarety == CardRarety.Rare)
            {
                rareButton.GetComponent<Image>().color = new Color(1f, 1f, 1f);
            }
            else if (cardRarety == CardRarety.UnCommon)
            {
                uncoButton.GetComponent<Image>().color = new Color(1f, 1f, 1f);
            }
            else if (cardRarety == CardRarety.Common)
            {
                coButton.GetComponent<Image>().color = new Color(1f, 1f, 1f);
            }
        }
    }
    public void SearchByText()
    {
        csoptions.containText = searchInputField.text;
        cardGridScript.PopulateByOptions(csoptions);
    }
    #endregion SortMethods

    public void InitCollectionBox()
    {
        cardGridScript.PopulateByOptions(csoptions);
    }


    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(1);
    }


}
