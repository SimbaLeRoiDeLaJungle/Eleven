using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterCard : MonoBehaviour
{
    Animator animator;

    CardInitializer initializer;

    void Awake()
    {
        animator = GetComponent<Animator>();
        initializer = GetComponent<CardInitializer>();
        ShowCard();
    }

    public void Init(CardScriptable script)
    {
        initializer.SetCardScriptable(script);
        initializer.UpdateRender();
    }

    public void ShowCard()
    {
        animator.SetTrigger("show");
    }

    public void HideCard()
    {
        animator.SetTrigger("hide");
    }
}
