using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterOpening : MonoBehaviour
{
    [SerializeField]
    GameObject cardPrefab;
    Animator animator;
    [SerializeField]
    GameObject CardsContnairGO;
    [SerializeField]
    SerieScriptable serie;

    List<CardScriptable> cards = new List<CardScriptable>();
    int count = -1;

    BoosterCard lastCard = null;
    // Start is called before the first frame update

    void Reset()
    {
        Debug.Log("reset");
        lastCard = null;
        foreach(Transform t in CardsContnairGO.transform)
        {
            Destroy(t.gameObject);
        }
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        cards = serie.GetBooster();
    }

    // Update is called once per frame
    void Update()
    {
        if(count < cards.Count)
        {
            if(Input.GetMouseButtonDown(0))
            {
                if(count == -1)
                {
                    animator.SetTrigger("launch");
                }
                else
                {
                    if(lastCard != null)
                    {
                        lastCard.HideCard();
                    }
                    var go = Instantiate(cardPrefab);
                    go.transform.SetParent(CardsContnairGO.transform, false);
                    go.name = "Card_" + count.ToString();
                    go.transform.SetSiblingIndex(count);
                    var cardScript = go.GetComponent<BoosterCard>();
                    cardScript.Init(cards[count]);
                    lastCard = cardScript;
                }
                count++;
            }
        }

    }
}
