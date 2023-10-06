using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardUpdater : MonoBehaviour
{    
    float time= 0;
    float updateTime =0.03f;

    [SerializeField]
    Image img;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (img.material.name == "FA_holo")
        {
            float dparalax = 0.005f;
            time += Time.deltaTime;
            string propName = "_RainbowOffset";
            float max = 1;
            if(time >= updateTime)
            {
                Vector4 paralax = img.material.GetVector(propName);
                paralax.x += dparalax;
                if(paralax.x >= 1f)
                {
                    paralax.x = 0f;
                }
                img.material.SetVector(propName,paralax);
                time = 0;
            }
        }

    }
}
