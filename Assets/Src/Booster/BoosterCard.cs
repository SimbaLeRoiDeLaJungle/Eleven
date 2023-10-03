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
        initializer.UpdateRender(true);
    }

    public void ShowCard()
    {
        animator.SetTrigger("show");
    }

    public void HideCard()
    {
        animator.SetTrigger("hide");
    }

    public bool IsAnimated()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return !stateInfo.IsName("Base Layer.Base_State");
    }
}
