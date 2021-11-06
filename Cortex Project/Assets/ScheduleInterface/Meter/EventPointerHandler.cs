using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPointerHandler : MonoBehaviour
{
    public TimelineHandler timelineHandler;
    public Transform meterTransform;

    RectTransform rectTrans;
    // Start is called before the first frame update
    void Start()
    {
        rectTrans = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timelineHandler.timelineData.currentIndex == timelineHandler.timelineData.eventsInSequence.Count)
        {
            if (timelineHandler.timelineData.currentIndex == 0) // the events sequence is empty
            {
                transform.position = new Vector3(meterTransform.position.x, transform.position.y);
            } else // the pointer is pointing to the end of the sequence
            {

                Transform objectToSurpass = meterTransform.GetChild(timelineHandler.timelineData.currentIndex - 1).transform;
                rectTrans.anchoredPosition = new Vector2(((RectTransform)meterTransform).anchoredPosition.x + ((RectTransform)objectToSurpass).sizeDelta.x + ((RectTransform)objectToSurpass).anchoredPosition.x, rectTrans.anchoredPosition.y);
            }
        }
        else
        {
            RectTransform itemToPointAt = meterTransform.GetChild(timelineHandler.timelineData.currentIndex).GetComponent<RectTransform>();
            rectTrans.position = itemToPointAt.position;
        }
    }
}
