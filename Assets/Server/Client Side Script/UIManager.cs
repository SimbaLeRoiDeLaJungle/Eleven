using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField]
    TMP_InputField userName;
    [SerializeField]
    TMP_InputField password;
    [SerializeField]
    CreateAcountPannel createAcountPannel;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Destroy(this);
        }
    }

    void Start()
    {

    }

    public void ConnectUser()
    {
        using (Packet packet = new Packet((int)ClientPackets.loginRequest))
        {
            packet.Write(Client.instance.myId);
            packet.Write(userName.text);
            packet.Write(password.text);
            ClientSend.SendTCPData(packet);
        }
    }

    public void AddUser()
    {
        if (createAcountPannel.Password1 == createAcountPannel.Password2)
        {
            using (Packet packet = new Packet((int)ServerPackets.createUser))
            {
                packet.Write(Client.instance.myId);
                packet.Write(createAcountPannel.Username);
                packet.Write(createAcountPannel.Password1);
                packet.Write(createAcountPannel.Email);
                ClientSend.SendTCPData(packet);
            }
        }
    }
}
