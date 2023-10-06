using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.ShaderData;

public class BackgroundUpdater : MonoBehaviour
{
    float time = 0;
    float updateTime = 0.03f;

    [SerializeField]
    Image img;

    [SerializeField]
    Texture2D maskTexture1;    
    [SerializeField]
    Texture2D maskTexture2;

    int currentMask = 1;
    bool pass = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (img.material.name == "BackgroundMat")
        {
            float dparalax = 0.00055f;
            time += Time.deltaTime;
            string propName = "_maskOffset";
            float max = 0.9f;
            float min = 0.75f;
            if (time >= updateTime)
            {
                Vector4 paralax = img.material.GetVector(propName);
                if (pass)
                {
                    paralax.x += dparalax;
                    if (paralax.x >= max)
                    {
                        paralax.x = max;
                        pass = false;

                    }
                    img.material.SetVector(propName, paralax);
                }
                else
                {
                    paralax.x -= dparalax;
                    if (paralax.x <= min)
                    {
                        paralax.x = min;
                        pass = true;
                        if (currentMask == 1)
                        {
                            img.material.SetTexture("_MaskTex", maskTexture2);
                            currentMask = 2;
                        }
                        else
                        {
                            img.material.SetTexture("_MaskTex", maskTexture1);
                            currentMask = 1;
                        }

                    }
                    img.material.SetVector(propName, paralax);

                        
                }
                time = 0;
            }
        }
    }
}
