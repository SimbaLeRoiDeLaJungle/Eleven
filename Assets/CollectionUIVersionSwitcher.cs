using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectionUIVersionSwitcher : MonoBehaviour
{
    [SerializeField]
    GameObject buttonPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateButton(CardRarety rarety, string serieCode, int cardId )
    {
        if(rarety == CardRarety.Common)
        {
            var go = Instantiate(buttonPrefab, this.transform);
            var transform = go.GetComponent<Transform>().GetChild(0);
            transform.gameObject.GetComponent<TMP_Text>().text = $"{serieCode} {cardId} (CO)";
        }
        else if(rarety == CardRarety.UnCommon)
        {
            var go = Instantiate(buttonPrefab, this.transform);
            var transform = go.GetComponent<Transform>().GetChild(0);
            transform.gameObject.GetComponent<TMP_Text>().text = $"{serieCode} {cardId} (UNCO)";
        }
        else if(rarety == CardRarety.Rare)
        {
            var go = Instantiate(buttonPrefab, this.transform);
            var transform = go.GetComponent<Transform>().GetChild(0);
            transform.gameObject.GetComponent<TMP_Text>().text = $"{serieCode} {cardId} (Rare)";
        }
        else if(rarety == CardRarety.FA)
        {
            var go = Instantiate(buttonPrefab, this.transform);
            var transform = go.GetComponent<Transform>().GetChild(0);
            transform.gameObject.GetComponent<TMP_Text>().text = $"{serieCode} {cardId} (FA)";
        }
        else if(rarety == CardRarety.ALT)
        {
            var go = Instantiate(buttonPrefab, this.transform);
            var transform = go.GetComponent<Transform>().GetChild(0);
            transform.gameObject.GetComponent<TMP_Text>().text = $"{serieCode} {cardId} (ALT)";
        }
        
    }
    public void Set(List<CardScriptable> sameVersions)
    {
        foreach(var card in sameVersions) 
        {
            CreateButton(card.rarety, SeriesData.GetSerieCode(card.serieNumber), card.number);
        }
    }
    public void Clear()
    {
        foreach(Transform t in transform)
        {
            Destroy(t.gameObject);
        }
    }
}
