using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMenu : MonoBehaviour
{
    CardInitializer initializer;
    void Start()
    {
        initializer = GetComponent<CardInitializer>();
        initializer.UpdateRender(true);
    }
}
