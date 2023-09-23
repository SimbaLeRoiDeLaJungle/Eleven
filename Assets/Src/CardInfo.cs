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
}
