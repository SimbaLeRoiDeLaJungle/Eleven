using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClientHandle : MonoBehaviour
{
    public static void Welcome(Packet _packet)
    {
        string _msg = _packet.ReadString();
        int _myId = _packet.ReadInt();

        Debug.Log($"Message from server: {_msg}");
        Client.instance.myId = _myId;
        ClientSend.WelcomeReceived();
    }

    public static void CreateUser(Packet _packet)
    {
        int _myId = _packet.ReadInt();
        bool _isAccept = _packet.ReadBool();
        if (_isAccept)
        {
            Debug.Log("Ton compte à été créer");
        }
        else
        {
            Debug.Log("Ton compte n'as pas pu etre créer ... ");
        }
    }

    public static void LoginResponse(Packet _packet)
    {
        int _myId = _packet.ReadInt();
        int _dbId = _packet.ReadInt();
        string username = _packet.ReadString();

        if(_dbId<0)
        {
            Debug.Log("Mauvais Nom d'utilisateur ou mot de passe");
        }
        else
        {
            Debug.Log($"Connection effectuer : DB_ID = {_dbId}");
            Client.instance.db_id = _dbId;
            Client.instance.username = username;
            SceneManager.LoadScene(1);
        }
    }

    public static void UpdateCollection(Packet _packet)
    {
        List<CardAndCount> data = new List<CardAndCount>();
        
        int cash = _packet.ReadInt();

        int Length = _packet.ReadInt();
        for(int i = 0; i < Length; i++) 
        {
            int card_id = _packet.ReadInt();
            int serie_id = _packet.ReadInt();
            int count = _packet.ReadInt();
            int in_trade = _packet.ReadInt();
            Debug.Log(card_id);
            Debug.Log(serie_id);
            Debug.Log(count);
            Debug.Log(in_trade);
            CardScriptable script = SeriesData.instance.GetCard(serie_id, card_id);
            if(script != null)
            {
                data.Add(new CardAndCount(script, count,in_trade));
            }
            
        }
        data.Sort();
        Client.instance.SetCollection(data);
        Client.SetCash(cash);

        Client.instance.SetIsReady(true);
    }

    public static void UpdateTradeList(Packet _packet)
    {
        if(TradeListUpdater.instance == null)
        {
            return;
        }
        bool isLast = !_packet.ReadBool();
        List<TradeData> datas = new List<TradeData>();
        int i = 0;
        while(!isLast)
        {
            int trade_id = _packet.ReadInt();

            string ownerName = _packet.ReadString();

            int price = _packet.ReadInt();
            int year = _packet.ReadInt();
            
            int month = _packet.ReadInt();
            int day = _packet.ReadInt();
            
            int hour = _packet.ReadInt();
            int minute = _packet.ReadInt();
            int second = _packet.ReadInt();

            DateTime postDate = new DateTime(year,month, day,hour,minute,second);

            bool isLastCard = false;

            List<CardAndCount> cards = new List<CardAndCount>();

            while(!isLastCard) 
            {
                int serie_id= _packet.ReadInt();
                Debug.Log($"serie id : {serie_id}");
                int card_id = _packet.ReadInt();
                int count = _packet.ReadInt();
                cards.Add(new CardAndCount(SeriesData.instance.GetCard(serie_id,card_id),count));
                isLastCard = _packet.ReadBool();
            }

            datas.Add(new TradeData(trade_id,ownerName ,price, postDate, cards));
            
            isLast = _packet.ReadBool();
        }
        TradeListUpdater.instance.Add(datas);
    }

}