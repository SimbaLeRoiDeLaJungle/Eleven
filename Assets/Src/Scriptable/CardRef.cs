using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardRef : ScriptableObject
{
    public string cardName;
    public CardType cardType;
    public string cardRef;
    public virtual string GetEffect(int i)
    {
        return "";
    }
}
