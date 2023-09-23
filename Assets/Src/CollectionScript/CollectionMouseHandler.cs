using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CollectionMouseHandler : MonoBehaviour,IPointerClickHandler
{
    [SerializeField]
    CollectionManager collecManager;

    CardGridScript cardGridScript;

    public void OnPointerClick(PointerEventData eventData)
    {
        var ccb = eventData.pointerCurrentRaycast.gameObject.GetComponent<CollectionCardButton>();
        this.collecManager.SetCardWatcherMode(ccb.GetCardScriptable());
    }

    public void SetCollectionManager(CollectionManager collectionManager)
    {
        this.collecManager = collectionManager;
    }
}
