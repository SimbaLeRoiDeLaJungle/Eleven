using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Sort Ref", menuName = "Card/new Sort reference")]
public class SortRef : CardRef
{
    [TextAreaAttribute]
    public string effect;
    
    public string GetEffect(int i)
    {
        return effect;
    } 
}
