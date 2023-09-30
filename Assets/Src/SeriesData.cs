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
}

