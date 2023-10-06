using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSend : MonoBehaviour
{
    private static void SendTCPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.tcp.SendData(_packet);
    }

    #region Packets
    public static void WelcomeReceived()
    {
        using (Packet _packet = new Packet((int)ClientPackets.welcomeReceived))
        {
            _packet.Write(Client.instance.myId);
            _packet.Write("receive data");

            SendTCPData(_packet);
        }
    }

    public static void AddCards(List<CardScriptable> _cards, int serie_id=-1)
    {
        using (Packet _packet = new Packet((int)ClientPackets.addCard))
        {
            int i = 0;
            foreach (CardScriptable card in _cards) 
            {
                _packet.Write(card.number);
                _packet.Write(card.serieNumber);
                i += 1;
                _packet.Write(i >= _cards.Count); // is This the last card ?
            }
            _packet.Write(serie_id!=-1);
            _packet.Write(serie_id);
            SendTCPData(_packet);
        }
    }

    public static void RequestUpdateCollection()
    {
        Client.instance.SetIsReady(false);
        using(Packet _packet = new Packet((int)ClientPackets.updateCollectionRequest))
        {
            SendTCPData(_packet);
        }
        
    }

    public static void LoginRequest(string username, string password)
    {
        using (Packet packet = new Packet((int)ClientPackets.loginRequest))
        {
            packet.Write(Client.instance.myId);
            packet.Write(username);
            packet.Write(password);
            ClientSend.SendTCPData(packet);
        }
    }

    public static void CreateUserRequest(string username, string password, string email)
    {
        using (Packet packet = new Packet((int)ServerPackets.createUser))
        {
            packet.Write(Client.instance.myId);
            packet.Write(username);
            packet.Write(password);
            packet.Write(email);
            ClientSend.SendTCPData(packet);
        }
    }

    public static void CreateTradeRequest(List<CardAndCount> cards, int price)
    {
        using(Packet packet = new Packet((int)ClientPackets.createTrade))
        {
            packet.Write(Client.instance.myId);
            packet.Write(price);
            for(int i =0; i < cards.Count; i++)
            {
                packet.Write(cards[i].script.serieNumber);
                packet.Write(cards[i].script.number);
                packet.Write(cards[i].count);
                packet.Write(i==cards.Count-1);
            }

            ClientSend.SendTCPData(packet);
        }
    }

    public static void RequestTradeData(DateTime beginSearch, DateTime endSearch, int last_id) 
    {
        using (Packet packet = new Packet((int)ClientPackets.requestTradeData))
        {
            packet.Write(Client.instance.myId);

            packet.Write(beginSearch.Year);
            packet.Write(beginSearch.Month);
            packet.Write(beginSearch.Day);
            packet.Write(beginSearch.Hour);
            packet.Write(beginSearch.Minute);
            packet.Write(beginSearch.Second);

            packet.Write(endSearch.Year);
            packet.Write(endSearch.Month); 
            packet.Write(endSearch.Day);
            packet.Write(endSearch.Hour);
            packet.Write(endSearch.Minute);
            packet.Write(endSearch.Second);
            
            packet.Write(last_id);

            ClientSend.SendTCPData(packet);
        }
    }
    #endregion
}