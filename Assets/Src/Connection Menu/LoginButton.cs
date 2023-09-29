using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LoginButton : MonoBehaviour
{
    [SerializeField]
    TMP_Text userName;
    [SerializeField]
    TMP_Text password;
    Button button;


    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => Client.instance.ConnectToServer());
        
    }
}
