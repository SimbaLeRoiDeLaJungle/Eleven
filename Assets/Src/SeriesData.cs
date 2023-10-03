using System;
using System.Collections.Generic;
using UnityEngine;

public class SeriesData : MonoBehaviour
{
    [SerializeField]
    List<SerieScriptable> series;
    public static SeriesData instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } 
        else if (instance != this)
        {
            Destroy(this);
        }
    }

    public CardScriptable GetCard(int serie_id, int card_id)
    {
        return series[serie_id].GetCard(card_id);
    }

    public List<SerieScriptable> GetSeries()
    {
        return series;
    }

    public List<CardScriptable> FindRarityByRef(CardScriptable cardScriptable)
    {
        List<CardScriptable> result = new List<CardScriptable>();

        for(int i = 0; i < series.Count; i++)
        {
            List<CardScriptable> serie = series[i].GetCards();
            foreach (CardScriptable card in serie)
            {
                if(card != cardScriptable)
                {
                    if(card.cardRef.cardRef == cardScriptable.cardRef.cardRef)
                    {
                        result.Add(card);
                    }
                }
            }
        }


        return result;
    }

    public static string GetSerieCode(int serieNumber)
    {
        return instance.series[serieNumber].Code;
    }
}

