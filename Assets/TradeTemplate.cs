using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TradeTemplate : MonoBehaviour
{
    [SerializeField]
    TMP_Text username;
    
    [SerializeField]
    TMP_Text cards;
    
    [SerializeField]
    TMP_Text date;   
    
    [SerializeField]
    TMP_Text price;

    [SerializeField]
    Button interaction;

    public delegate void TradeTemplateInteractionDel(TradeData _data);

    TradeData data;
    public void SetInfo(TradeData _data)
    {
        data = _data;

        username.text = data.GetOwnerName();
        cards.text = data.GetCardsToString();
        date.text = data.postDate.ToString("HH:mm - dd/MM/yyyy");
        price.text = data.price.ToString();
    }

    public void SetInteraction(TradeTemplateInteractionDel tti)
    {
        interaction.onClick.AddListener(() => tti(data));
    }
    


}
