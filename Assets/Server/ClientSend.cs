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

    public static void AddCard(CardScriptable _card)
    {
        using(Packet _packet = new Packet((int)ClientPackets.addCard))
        {
            _packet.Write(_card.number);
            _packet.Write(_card.serieNumber);
            _packet.Write(false);

            SendTCPData(_packet);
        }
    }

    public static void AddCards(List<CardScriptable> _cards)
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
    #endregion
}