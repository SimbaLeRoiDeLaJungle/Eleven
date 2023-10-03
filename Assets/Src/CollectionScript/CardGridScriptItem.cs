using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ----------------------- Card Grid Script Item -----------------------
// Les items dans la grid
//-----------------------------------------------------------------
public class CardGridScriptItem : MonoBehaviour
{
    [SerializeField]
    CardScriptable cardScriptable; // Info sur la cartes

    CardInitializer cardInitializer; // pour l'effet holo

    #region UnityMethods
    // Start is called before the first frame update
    void Awake()
    {
        cardInitializer  = GetComponent<CardInitializer>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    #endregion UnityMethods

    public void Init(CardScriptable cardScriptable)
    {
        this.cardScriptable = cardScriptable;
        cardInitializer.SetCardScriptable(cardScriptable);
        cardInitializer.UpdateRender(false);
    }

    public CardScriptable GetCardScriptable()
    {
        return cardScriptable;
    }

    public void SetInCollection(bool inCollection)
    {
        if(!inCollection)
        {
            GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
        }
        else
        {
            GetComponent<Image>().color = new Color(1,1,1);
        }
    }
}
