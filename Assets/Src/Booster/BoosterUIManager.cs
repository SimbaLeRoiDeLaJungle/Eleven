using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BoosterUIManager : MonoBehaviour
{
    [SerializeField]
    BoosterOpening boosterOpening;
    [SerializeField]
    Button openButton;
    [SerializeField]
    TMP_Text openButtonText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(boosterOpening.IsOver() && !openButton.interactable)
        {
            openButton.interactable = true;
            openButtonText.text = "Open a new one";
        }
    }

    public void OpenBooster()
    {
        if(boosterOpening.IsOver())
        {
            StartCoroutine(boosterOpening.OpenNew());
            openButtonText.text = "Open";
        }
        else
        {
            boosterOpening.LaunchOpening();
            openButton.interactable = false;
            ClientSend.AddCards(boosterOpening.ToList(),boosterOpening.SerieId);
        }

    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(1);
    }
}
