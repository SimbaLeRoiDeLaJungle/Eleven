using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CardGridScript : MonoBehaviour
{

    [SerializeField]
    CollectionManager collecManager;
    
    [SerializeField]
    List<CardScriptable> cardScripts;
    
    [SerializeField]
    GameObject prefabCard;

    [SerializeField]
    Vector2 padding;

    [SerializeField]
    Vector2 size;

    [SerializeField]
    Vector2 relativePosition;

    List<Card> cards;

    int currentIndex;
    // Start is called before the first frame update
    void Start()
    {
        Populate();
        collecManager.SetCardWatcherMode(cardScripts[0]);
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCurrentCard(CardScriptable script)
    {
        currentIndex=0;
        for(int i = 0; i<cardScripts.Count; i++)
        {
            if(script.GetCardName() == cardScripts[i].GetCardName())
            {
                currentIndex = i;
                break;
            }
        }
    }

    public CardScriptable Previous()
    {
        currentIndex--;
        if(currentIndex<0)
            currentIndex = cardScripts.Count-1;
        return cardScripts[currentIndex];
    }
    public CardScriptable Next()
    {
        currentIndex++;
        if(currentIndex>cardScripts.Count-1)
            currentIndex =0;
        return cardScripts[currentIndex];
    }

    public void Populate()
    {
        Reset();
        float x = relativePosition.x;
        float y = relativePosition.y;
        float width=0;
        for(int index=0; index<cardScripts.Count; index++)
        {
            var go = Instantiate(prefabCard,this.transform);

            CollectionCardButton btn = go.GetComponent<CollectionCardButton>();
            btn.Init(cardScripts[index]);
            CollectionMouseHandler mouseHandler = go.GetComponent<CollectionMouseHandler>();
            mouseHandler.SetCollectionManager(this.collecManager);
        } 
    }

    public void PopulateByCardType(CardType cardType)
    {
        Reset();
        float x = relativePosition.x;
        float y = relativePosition.y;
        float width=0;
        for(int index=0; index<cardScripts.Count; index++)
        {
            if(cardScripts[index].GetCardType() == cardType)
            {
                var go = Instantiate(prefabCard,this.transform);

                CollectionCardButton btn = go.GetComponent<CollectionCardButton>();
                btn.Init(cardScripts[index]);
                CollectionMouseHandler mouseHandler = go.GetComponent<CollectionMouseHandler>();
                mouseHandler.SetCollectionManager(this.collecManager);
            }

        }
    }
    public void PopulateByOptions(CollectionSortOptions csoptions)
    {
        Reset();
        float x = relativePosition.x;
        float y = relativePosition.y;
        float width=0;
        for(int index=0; index<cardScripts.Count; index++)
        {
            bool goodType = csoptions.Get(cardScripts[index].GetCardType());
            bool goodRarety = csoptions.Get(cardScripts[index].rarety);
            if(goodType && goodRarety)
            {
                var go = Instantiate(prefabCard,this.transform);

                CollectionCardButton btn = go.GetComponent<CollectionCardButton>();
                btn.Init(cardScripts[index]);
                CollectionMouseHandler mouseHandler = go.GetComponent<CollectionMouseHandler>();
                mouseHandler.SetCollectionManager(this.collecManager);
            }

        }
    }

    public void Reset()
    {
        foreach(Transform t in this.transform)
        {
            Destroy(t.gameObject);
        }
    }
}
