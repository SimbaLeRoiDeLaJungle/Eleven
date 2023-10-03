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

    public int SerieId{ get { return serie.serieId; } }

    List<CardScriptable> cards = new List<CardScriptable>();
    int count = 0;

    BoosterCard lastCard = null;

    bool openningIsStarted = false;

    bool isGetNew = false;
    // Start is called before the first frame update

    void Reset()
    {
        lastCard = null;
        foreach(Transform t in CardsContnairGO.transform)
        {
            Destroy(t.gameObject);
        }
        count = 0;
        cards = new List<CardScriptable>();
        openningIsStarted = false;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!openningIsStarted)
            return;

        if(count < cards.Count && animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Opening"))
        {
            if(Input.GetMouseButtonDown(0))
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
                count++;
            }
        }

    }

    public void LaunchOpening()
    {
        openningIsStarted = true;
        cards = serie.GetBooster();
        animator.SetTrigger("launch");
    }

    public bool isOpening()
    {
        return count < cards.Count && openningIsStarted;
    }

    public bool IsOver()
    {
        return count >= cards.Count && openningIsStarted;
    }

    public IEnumerator OpenNew()
    {
        if (lastCard != null)
        {
            isGetNew = true;
            lastCard.HideCard();
        
            float waitTime = 0.1f;
            while (isGetNew && lastCard != null)
            {
                if (!lastCard.IsAnimated())
                    isGetNew = false;

                yield return new WaitForSeconds(waitTime);
            }
        }
        Reset();
        animator.SetTrigger("new");
    }

    public List<CardScriptable> ToList()
    {
        return cards;
    }
}
