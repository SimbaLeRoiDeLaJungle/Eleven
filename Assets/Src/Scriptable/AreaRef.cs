using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Area Ref", menuName = "Card/new Area reference")]
public class AreaRef : CardRef
{
    [TextAreaAttribute]
    public string effectLvl1;
    
    [TextAreaAttribute]
    public string effectLvl2;

    [TextAreaAttribute]
    public string effectLvl3;

    public string GetEffect(int i)
    {
        if(i == 0)
            return effectLvl1;
        else if(i == 1)
            return effectLvl2;
        else
            return effectLvl3;
    }
}
