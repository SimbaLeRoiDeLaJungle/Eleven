using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


// Vien en complément de cardGridScript pour trier les element présents dans la grille plus simplement
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
        if (cardRarety == CardRarety.ALT)
        {
            return alt;
        }
        else if (cardRarety == CardRarety.FA)
        {
            return fa;
        }
        else if (cardRarety == CardRarety.Rare)
        {
            return rare;
        }
        else if (cardRarety == CardRarety.UnCommon)
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
        if (cardType == CardType.Area)
        {
            return showByArea;
        }
        else if (cardType == CardType.Heros)
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
        inCollection = !inCollection;
    }
    public void SwitchNotInCollection()
    {
        notInCollection = !notInCollection;
    }

    public void Set(CardType cardType, bool value)
    {
        if (cardType == CardType.Area)
        {
            showByArea = value;
        }
        else if (cardType == CardType.Heros)
        {
            showByHeros = value;
        }
        else if (cardType == CardType.Sort)
        {
            showBySort = value;
        }
    }
    public void Switch(CardType cardType)
    {
        if (cardType == CardType.Area)
        {
            showByArea = !showByArea;
        }
        else if (cardType == CardType.Heros)
        {
            showByHeros = !showByHeros;
        }
        else if (cardType == CardType.Sort)
        {
            showBySort = !showBySort;
        }

    }
    public void Switch(CardRarety cardRarety)
    {

        if (cardRarety == CardRarety.ALT)
        {
            alt = !alt;
        }
        else if (cardRarety == CardRarety.FA)
        {
            fa = !fa;
        }
        else if (cardRarety == CardRarety.Rare)
        {
            rare = !rare;
        }
        else if (cardRarety == CardRarety.UnCommon)
        {
            unco = !unco;
        }
        else if (cardRarety == CardRarety.Common)
        {
            co = !co;
        }
    }
}
public class SortManager : MonoBehaviour
{

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

    [SerializeField]
    Animator sortPanelAnimator;

    [SerializeField]
    Button showAndHidePanel;

    [SerializeField]
    TMP_InputField searchInputField;

    [SerializeField]
    Button searchButton;

    CollectionSortOptions csoptions = new CollectionSortOptions();

    CardGridScript cardGridScript;

    public void SetCardGridScript(CardGridScript _cardGridScript)
    {
        cardGridScript = _cardGridScript;
    }
    public void OpenCloseSortPanel()
    {

        if (sortPanelAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.CollectionSortPanelBaseAnim"))
        {
            sortPanelAnimator.SetTrigger("hide");
        }
        else if (sortPanelAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.CollectionSortPanelBaseHideAnim"))
        {
            sortPanelAnimator.SetTrigger("show");
        }
    }

    private void Start()
    {

        inCollectionButton.onClick.AddListener(() => Sort(true));
        notInCollectionButton.onClick.AddListener(() => Sort(false));

        UpdateSortButton(false);

        sortSortButton.onClick.AddListener(() => Sort(CardType.Sort));
        herosSortButton.onClick.AddListener(() => Sort(CardType.Heros));
        areaSortButton.onClick.AddListener(() => Sort(CardType.Area));

        altButton.onClick.AddListener(() => Sort(CardRarety.ALT));
        faButton.onClick.AddListener(() => Sort(CardRarety.FA));
        rareButton.onClick.AddListener(() => Sort(CardRarety.Rare));
        uncoButton.onClick.AddListener(() => Sort(CardRarety.UnCommon));
        coButton.onClick.AddListener(() => Sort(CardRarety.Common));

        searchButton.onClick.AddListener(() => SearchByText());

        showAndHidePanel.onClick.AddListener(() => OpenCloseSortPanel());
    }

    public void Sort(CardType cardType)
    {
        csoptions.Switch(cardType);
        cardGridScript.Populate(csoptions);
        UpdateSortButton(cardType);
    }

    public void Sort(CardRarety cardRarety)
    {
        csoptions.Switch(cardRarety);
        cardGridScript.Populate(csoptions);
        UpdateSortButton(cardRarety);
    }

    public void Sort(bool isInCollection)
    {
        if (isInCollection)
            csoptions.SwitchInCollection();
        else
            csoptions.SwitchNotInCollection();

        cardGridScript.Populate(csoptions);
        UpdateSortButton(isInCollection);
    }
    public void UpdateSortButton(bool isInCollection)
    {
        if (isInCollection)
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
        else
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
        cardGridScript.Populate(csoptions);
    }
}

