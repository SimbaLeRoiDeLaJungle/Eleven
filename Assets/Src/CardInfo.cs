using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CardInfo : MonoBehaviour
{
    [SerializeField]
    TMP_Text name;
    [SerializeField]
    TMP_Text heroEffect;
    [SerializeField]
    TMP_Text time;
    [SerializeField]
    TMP_Text power;
    [SerializeField]
    TMP_Text cost;
    [SerializeField]
    GameObject heroInfo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Set(CardScriptable cardScript)
    {
        if(cardScript.GetCardType() == CardType.Heros)
        {
            HerosRef data = (HerosRef)cardScript.cardRef;
            name.text = data.cardName;
            heroEffect.text = data.GetEffect(0);
            time.text = data.time.ToString();
            power.text = data.power.ToString();
            cost.text = data.cost.ToString();
        }
    }

    public void SetOnRect(Rect rect)
    {       
        Rect templateRect = GetComponent<RectTransform>().rect;
        GetComponent<RectTransform>().sizeDelta = new Vector2(rect.width, rect.height);


        float ratio = rect.width/ templateRect.width;

        UpdateText(name,ratio);
        UpdateText(heroEffect, ratio);
        UpdateText(time, ratio);
        UpdateText(power, ratio);
        UpdateText(cost, ratio);        
        
    }

    void UpdateText(TMP_Text text,float ratio)
    {
        text.transform.localPosition = text.transform.localPosition * ratio;
        text.fontSizeMax = text.fontSizeMax * ratio;
        text.fontSizeMin = text.fontSizeMin * ratio;
    }
}
