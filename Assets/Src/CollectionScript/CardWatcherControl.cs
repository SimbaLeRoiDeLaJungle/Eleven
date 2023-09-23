using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CardWatcherControl : MonoBehaviour
{
    [SerializeField]
    Card card;
    [SerializeField]
    float speed; 
    bool moussePressed = false;
    Vector3 cardCenter;

    public void SetCardScriptable(CardScriptable cardInfo)
    {
        card.SetCardScriptable(cardInfo);
    }

    void Start()
    {
        Rect cardRect = card.GetBound();
        float cardXCenter = cardRect.xMin + cardRect.width/2;
        float cardYCenter = cardRect.yMax - cardRect.height/2;
        this.cardCenter = new Vector3(cardXCenter,cardYCenter,card.transform.position.z);
    }

    void Update()
    {       
        if(Input.GetMouseButton(0))
            moussePressed = true;
        if(Input.GetMouseButtonUp(0))
            moussePressed = false;

        UpdateRotationX(moussePressed);
        UpdateRotationY(moussePressed);
        card.transform.localEulerAngles = new Vector3(card.transform.localEulerAngles.x,card.transform.localEulerAngles.y,0);
    }

    void UpdateRotationY(bool moussePressed)
    {   
        float angleOffset=1f;
        if (moussePressed)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Rect cardRect = card.GetBound();

            bool left = mousePosition.x >= cardRect.xMin;
            bool right = mousePosition.x <= cardRect.xMin + cardRect.width;
            bool top = mousePosition.y <= cardRect.yMax;
            bool down = mousePosition.y >= cardRect.yMax - cardRect.height;
   
            if(left && right && top && down)
            {
                float yEulerAngle = card.transform.localEulerAngles.y;
                float percent = (cardCenter.x-mousePosition.x)/(cardRect.width/2);
                float length = 30*Mathf.Abs(percent);
                bool leftLimit = (yEulerAngle > -length && yEulerAngle<=0) || (yEulerAngle > 360-length || yEulerAngle <= 180) ;
                bool rightLimit = yEulerAngle < length || yEulerAngle >=180;
                if(mousePosition.x < cardCenter.x && leftLimit)
                {
                    card.transform.RotateAround(cardCenter, -card.transform.up, speed);
                }
                else if( mousePosition.x > cardCenter.x && rightLimit)
                {
                    card.transform.RotateAround(cardCenter, card.transform.up, speed);
                }
            }
        }
        else
        {
            if(card.transform.localEulerAngles.y < -angleOffset || (card.transform.localEulerAngles.y < 360-angleOffset && card.transform.localEulerAngles.y > 180))
            {
                card.transform.RotateAround(cardCenter, card.transform.up, speed);
            }
            else if(card.transform.localEulerAngles.y > angleOffset)
            {
                card.transform.RotateAround(cardCenter, -card.transform.up, speed);
            }
            if ((card.transform.localEulerAngles.y > - angleOffset && card.transform.localEulerAngles.y< angleOffset) || (card.transform.localEulerAngles.y > 360-angleOffset))
            {
                card.transform.localEulerAngles = new Vector3(card.transform.localEulerAngles.x, 0, 0);
            }
        }
    }
    void UpdateRotationX(bool moussePressed)
    {
        float angleOffset = 1f;
        if (moussePressed)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Rect cardRect = card.GetBound();

            bool left = mousePosition.x >= cardRect.xMin;
            bool right = mousePosition.x <= cardRect.xMin + cardRect.width;
            bool top = mousePosition.y <= cardRect.yMax;
            bool down = mousePosition.y >= cardRect.yMax - cardRect.height;


            if(left && right && top && down)
            {
                float percent = (cardCenter.y-mousePosition.y)/(cardRect.height/2);
                float length = 30*Mathf.Abs(percent);
                float xEulerAngle = card.transform.localEulerAngles.x;
                bool topLimit = (xEulerAngle > -length && xEulerAngle<=0) || (xEulerAngle > 360-length || xEulerAngle <= 180) ;
                bool downLimit = xEulerAngle < length || xEulerAngle >=180;
                if(mousePosition.y < cardCenter.y && topLimit)
                {
                    card.transform.RotateAround(cardCenter, -card.transform.right, speed);
                }
                else if(!topLimit)
                {
                    card.transform.localEulerAngles = new Vector3(-length,card.transform.localEulerAngles.y,0);
                }
                if( mousePosition.y > cardCenter.y && downLimit)
                {
                    card.transform.RotateAround(cardCenter, card.transform.right, speed);
                }
                else if(!downLimit)
                {
                    card.transform.localEulerAngles = new Vector3(length,card.transform.localEulerAngles.y,0);
                }
            }
        }
        else
        {
            if(card.transform.localEulerAngles.x < -angleOffset || (card.transform.localEulerAngles.x < 360-angleOffset && card.transform.localEulerAngles.x > 180))
            {
                card.transform.RotateAround(cardCenter, card.transform.right, speed);
            }
            else if(card.transform.localEulerAngles.x > angleOffset)
            {
                card.transform.RotateAround(cardCenter, -card.transform.right, speed);
            }
            if ((card.transform.localEulerAngles.x > - angleOffset && card.transform.localEulerAngles.x< angleOffset) || (card.transform.localEulerAngles.x > 360-angleOffset))
            {
                card.transform.localEulerAngles = new Vector3(0,card.transform.localEulerAngles.y, 0);
            }
        }
    }


}
