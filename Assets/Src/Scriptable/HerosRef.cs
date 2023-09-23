using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Heros Ref", menuName = "Card/new Heros reference")]
public class HerosRef : CardRef
{

    public Side side;
    
    public int time;

    public int power;

    public int cost;

    [TextAreaAttribute]
    public string effect;

    public string GetEffect(int i)
    {
        return effect;
    }
}
