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
        Debug.Log("pass");
        List<CardAndCount> data = new List<CardAndCount>();
        int Length = _packet.ReadInt();
        for(int i = 0; i < Length; i++) 
        {
            int card_id = _packet.ReadInt();
            int serie_id = _packet.ReadInt();
            int count = _packet.ReadInt();
            CardScriptable script = SeriesData.instance.GetCard(serie_id, card_id);
            if(script != null)
            {
                data.Add(new CardAndCount(script, count));
            }
            
        }
        data.Sort();
        Client.instance.SetCollection(data);
        Client.instance.SetIsReady(true);
    }

}