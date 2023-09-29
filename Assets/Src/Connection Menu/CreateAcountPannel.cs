using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreateAcountPannel : MonoBehaviour
{
    Animator animator;
    [SerializeField]
    Button createAcountButton;
    [SerializeField]
    TMP_InputField username;
    [SerializeField]
    TMP_InputField email;
    [SerializeField]
    TMP_InputField password1;
    [SerializeField]
    TMP_InputField password2;
    [SerializeField]
    Button createAcountButton2;

    public string Username{get{return username.text;}}
    public string Email { get { return email.text;}}
    public string Password1 { get { return password1.text;}}
    public string Password2 { get { return password2.text;}}
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(password1.text != "" && password2.text != "")
        {
            if(password1.text != password2.text)
            {
                password1.gameObject.GetComponent<Image>().color = new Color(1f,0f,0f);
                password2.gameObject.GetComponent<Image>().color = new Color(1f,0f,0f);
            }
            else
            {
                password1.gameObject.GetComponent<Image>().color = new Color(1f,1f,1f);
                password2.gameObject.GetComponent<Image>().color = new Color(1f,1f,1f);
            }
        }
        else
        {
                password1.gameObject.GetComponent<Image>().color = new Color(1f,1f,1f);
                password2.gameObject.GetComponent<Image>().color = new Color(1f,1f,1f);
        }
    }

    public void ShowPannel()
    {
        animator.SetTrigger("show");
    }
}
