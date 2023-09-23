using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardRarety
{
    Common,
    UnCommon,
    Rare,
    FA,
    ALT
}
public enum CardType
{
    Heros,
    Sort,
    Area,
    Gemme
}
public enum Side
{
    Bleu,
    Rouge,
    Gris,
    Jaune,
}
[CreateAssetMenu(fileName = "Card", menuName = "Card/new Card")]
public class CardScriptable : ScriptableObject
{

    public CardRef cardRef;

    public int number;

    public int serieNumber;
    
    public CardRarety rarety;

    public Sprite sprite;

    public Sprite maskSprite;

    public Material material;

    public string GetCardName()
    {
        return cardRef.cardName;
    }
    
    public CardType GetCardType()
    {
        return cardRef.cardType;
    }

    public string GetEffect(int i)
    {
        return cardRef.GetEffect(i);
    }
}

