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
    TMP_Text username;
    [SerializeField]
    TMP_Text email;
    [SerializeField]
    TMP_InputField password1;
    [SerializeField]
    TMP_InputField password2;
    [SerializeField]
    Button createAcountButton2;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        createAcountButton.onClick.AddListener(() => ShowPannel());
        createAcountButton2.onClick.AddListener(() => AddUser());
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

    void ShowPannel()
    {
        animator.SetTrigger("show");
    }

    void AddUser()
    {
        if(password1.text == password2.text)
            StartCoroutine(DBManager.AddUserToDB(username.text,email.text,password1.text));
    }
}
