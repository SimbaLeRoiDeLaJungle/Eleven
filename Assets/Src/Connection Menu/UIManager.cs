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
        ClientSend.LoginRequest(userName.text, password.text);
    }

    public void AddUser()
    {
        if (createAcountPannel.Password1 == createAcountPannel.Password2)
        {
            ClientSend.CreateUserRequest(createAcountPannel.Username, createAcountPannel.Password1, createAcountPannel.Email);
        }
    }
}
