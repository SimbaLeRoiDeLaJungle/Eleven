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

        if(_dbId<0)
        {
            Debug.Log("Mauvais Nom d'utilisateur ou mot de passe");
        }
        else
        {
            Debug.Log($"Connection effectuer : DB_ID = {_dbId}");
            SceneManager.LoadScene(1);
        }
    }

}