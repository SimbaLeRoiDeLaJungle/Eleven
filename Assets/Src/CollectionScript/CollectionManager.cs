using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CollectionManager : MonoBehaviour
{
    public static CollectionManager instance;

    [SerializeField]
    Button leftButton;
    [SerializeField]
    Button rightButton;

    [SerializeField]
    CardGridScript cardGridScript;

    [SerializeField]
    CardWatcherControl cardWatcher;

    CardScriptable currentCard;

    [SerializeField]
    TMP_Text cardCount;

    [SerializeField]
    CollectionUIVersionSwitcher collectionUIVersionSwitcher;

    [SerializeField]
    SortManager sortManager;

    public void SetCardWatcherCard(CardScriptable cardScriptable)
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

        cardGridScript.SetHandleClick(HandleCardClick);

        sortManager.SetCardGridScript(cardGridScript);

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

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void HandleCardClick(CardGridScriptItem ccb)
    {
        SetCardWatcherCard(ccb.GetCardScriptable());

    }
}
