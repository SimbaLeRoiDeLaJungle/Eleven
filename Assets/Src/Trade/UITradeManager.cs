using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UITradeManager : MonoBehaviour
{
    [SerializeField]
    CreateTradePanel createTradePanel;

    [SerializeField]
    Button createTradeButton;

    [SerializeField]
    TMP_Text userCash;

    // Start is called before the first frame update
    void Start()
    {
        createTradeButton.onClick.AddListener(() => OpenCreateTradePanel());
        if(Client.instance!= null)
        {
            UpdateUserCash(Client.GetCash());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenCreateTradePanel()
    {
        createTradePanel.gameObject.SetActive(true);
    }    

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void UpdateUserCash(int cash)
    {
        userCash.text = cash.ToString();
    }
}
