using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionCardButton : MonoBehaviour
{
    [SerializeField]
    CardScriptable cardScriptable;
    Image image;
    // Start is called before the first frame update
    void Awake()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(CardScriptable cardInfo)
    {
        this.cardScriptable = cardInfo;
        this.image.sprite = this.cardScriptable.sprite;
    }

    public Rect GetRect()
    {
        return GetComponent<RectTransform>().rect;
    }

    public CardScriptable GetCardScriptable()
    {
        return cardScriptable;
    }
}
