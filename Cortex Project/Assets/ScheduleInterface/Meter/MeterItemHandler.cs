using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MeterItemHandler : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public Image image;

    [HideInInspector]
    public ButtonDisplayHandler hoverHandler;

    // public int orderInList;
    public RectTransform rectTrans;
    public bool destroyable;

    static Transform descriptorTransform;
    static Text descriptorText;
    // Start is called before the first frame update
    void Start()
    {
        if (descriptorTransform == null)
        {
            descriptorTransform = GameManager.current.timelineHandler.transform.Find("SpeechBubbleContainer");
            descriptorTransform.gameObject.SetActive(false);
            if (descriptorText == null)
            {
                descriptorText = descriptorTransform.GetChild(0).GetChild(0).GetComponent<Text>();
            }
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    ActionEventData dataShown;
    public void InitializeValues(ActionEventData displayData, ButtonDisplayHandler clickedButton)
    {
        hoverHandler = clickedButton;
        // this.orderInList = orderInList;
        image.color = displayData.color;
        dataShown = displayData;
        rectTrans.sizeDelta = new Vector2(displayData.minutesTaken / 5, rectTrans.rect.height); // 1 pixel per 5 minutes
        rectTrans.GetComponent<Image>().color = displayData.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        int hours = dataShown.minutesTaken / 60;
        int remainingMins = dataShown.minutesTaken % 60;
        descriptorText.text = dataShown.actionEnum.ToString() + " " + dataShown.nameText + " for " + hours.ToString() + " hrs " + remainingMins.ToString() + " min";
        descriptorTransform.position = transform.position + Vector3.right * rectTrans.sizeDelta.x / 2 + Vector3.up * 10;
        descriptorTransform.gameObject.SetActive(true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!destroyable) return;

        SoundManager.current.Click();
        hoverHandler.myButton.interactable = true;
        if (destroyable)
        {
            DestroyMeterItem();
        }
        // Reset data, which in this case will return display data to its variable state
        // by setting displayMinutes to 0.
        if (hoverHandler != null)
        {
            hoverHandler.ResetData();
        }
        descriptorTransform.gameObject.SetActive(false);

    }
    public void DestroyMeterItem()
    {
        
        EventsManager.current.EventItemDestroyed(transform.GetSiblingIndex());
        Destroy(gameObject);
         
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        descriptorTransform.gameObject.SetActive(false);
    }

    
}
