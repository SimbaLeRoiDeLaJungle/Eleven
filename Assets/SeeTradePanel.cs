using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SeeTradePanel : MonoBehaviour
{

    public static SeeTradePanel instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    [SerializeField]
    Button closeButton;

    [SerializeField]
    TMP_Text username;

    [SerializeField]
    TMP_Text price;

    [SerializeField]
    TMP_Text date;

    [SerializeField]
    CardGridScript cardGrid;

    TradeData data;

    // Start is called before the first frame update
    void Start()
    {
        closeButton.onClick.AddListener(() => Close());
        Close();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void Set(TradeData _data)
    {
        cardGrid.Reset();
        data = _data;
        username.text = data.trade_id.ToString();
        price.text = data.price.ToString();
        date.text = data.postDate.ToString("HH:mm - dd/MM/yyyy");
        foreach(var c in data.cards)
        {
            var script = (TradeGridItem)cardGrid.AddAtTheEnd(c.script);
            script.SetLockMode(true, c.count);
        }
        
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }    
    public void Close()
    {
        gameObject.SetActive(false);
    }
}
