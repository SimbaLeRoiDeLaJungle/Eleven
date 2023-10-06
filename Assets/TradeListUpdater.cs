using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TradeListUpdater : MonoBehaviour
{
    public static TradeListUpdater instance;
    public const float timeForUpdate = 10;
    float time = 0f;
    DateTime updateEndAt;
    DateTime updateStartAt;
    int lastId = 0;
    List<TradeData> datas = new List<TradeData>();

    [SerializeField]
    GameObject tradeItemPrefab;

    [SerializeField]
    GameObject goContnair;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        updateStartAt = DateTime.Now;
        updateEndAt = DateTime.Now;
        time = timeForUpdate*9/10;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time > timeForUpdate) 
        {
            ClientSend.RequestTradeData(updateStartAt, updateEndAt, lastId);
            time = 0f;
            Debug.Log("Request Send");
        }
    }

    public void Add(List<TradeData> _data)
    {
        foreach(TradeData d in _data)
        {
            datas.Add(d);

        }
        //TODO : sort by date
        if(_data.Count > 0)
        {
            datas.Sort();
            updateEndAt = datas[datas.Count - 1].postDate;
            updateStartAt = DateTime.Now;
            UpdateTradeTemplates();
        }
            
    }

    public void UpdateTradeTemplates()
    {
        foreach(Transform t in goContnair.transform)
        {
            Destroy(t.gameObject);
        }
        foreach(var d in datas)
        {
            var go = Instantiate(tradeItemPrefab, goContnair.transform);

            var script = go.GetComponent<TradeTemplate>();

            script.SetInteraction((TradeData _data) => TradeTemplateInteraction(_data));

            script.SetInfo(d);
        }

    }

    public void TradeTemplateInteraction(TradeData _data)
    {
        SeeTradePanel.instance.Set(_data);
        SeeTradePanel.instance.Open();
    }
}

public class TradeData: IComparable<TradeData>,IEquatable<TradeData>
{
    public int trade_id;
    public DateTime postDate;
    public List<CardAndCount> cards;
    public int price;
    public string ownerName;
    public TradeData(int _trade_id, string _ownerName, int _price, DateTime _postDate, List<CardAndCount> _cards)
    {
        trade_id = _trade_id;
        postDate = _postDate;
        price = _price;
        cards = _cards;
        ownerName = _ownerName;
    }

    public int CompareTo(TradeData other)
    {
        if (postDate < other.postDate)
            return 1;
        else if(postDate > other.postDate) 
            return -1;
        else
            return 0;
    }

    public bool Equals(TradeData other)
    {
        return this.postDate == other.postDate;
    }

    public string GetCardsToString()
    {
        string re = "";
        int i = 0;
        foreach(CardAndCount c in cards)
        {
            re += $"{c.count} x {c.script.GetCardName()}";
            if(i!= cards.Count-1)
            {
                re += ", ";
            }
            i++;
        }
        return re;
    }

    public string GetOwnerName()
    {
        return ownerName;
    }
}