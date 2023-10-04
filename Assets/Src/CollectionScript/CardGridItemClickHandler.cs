using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// ----------------------- Card Grid Item Click Handler----------------
// Permet d'implémenter ce qui ce passe lorsque l'on click sur un item de la grille dans differents contextes
//  -------------------------------------------------------------------
public class CardGridItemClickHandler : MonoBehaviour,IPointerClickHandler
{
    private CardGridScript.HandleClick handleClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        var ccb = eventData.pointerCurrentRaycast.gameObject.GetComponent<CardGridScriptItem>();
        if(ccb!= null && handleClick!=null) 
        {
            handleClick(ccb);
        }
        
    }

    public void SetHandleClick(CardGridScript.HandleClick _handleClick)
    {
        this.handleClick = _handleClick;
    }
}
