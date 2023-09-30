using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadShop()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadCollection()
    {
        StartCoroutine(WaitForLoadCollectionScene());
    }

    IEnumerator WaitForLoadCollectionScene()
    {
        ClientSend.RequestUpdateCollection();
        float waitTime = 0.1f;
        while(!Client.instance.GetIsReady())
        {
            yield return new WaitForSeconds(waitTime);
        }
        SceneManager.LoadScene(0);
    }
}
