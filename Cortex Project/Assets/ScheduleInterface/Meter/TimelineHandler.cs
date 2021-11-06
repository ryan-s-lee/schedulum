using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimelineHandler : MonoBehaviour
{
    public GameObject barImageObject;
    // Represents the location of the pointer
    // Start is called before the first frame update
    void Start()
    {
        EventsManager.current.onObjectClick += OnObjectCliked;
        EventsManager.current.onEventItemDestroyed += timelineData.RemoveActionEvent;
        timelineData.currentIndex = 1;
        CreateSleepBar(120); // At the beginning the player wakes up at 7
    }

    private void OnDestroy()
    {
        EventsManager.current.onObjectClick -= OnObjectCliked;
        EventsManager.current.onEventItemDestroyed -= timelineData.RemoveActionEvent;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.current.dayIsPlaying)
        {
            if (Input.GetKeyDown(KeyCode.A) && timelineData.currentIndex > 1)
            {
                timelineData.currentIndex--; // move down the index
            }
            else if (Input.GetKeyDown(KeyCode.D) && timelineData.currentIndex < timelineData.eventsInSequence.Count)
            {
                timelineData.currentIndex++; // move up the index
            }
        }
        timelineData.currentIndex = Mathf.Clamp(timelineData.currentIndex, 1, timelineData.eventsInSequence.Count);

    }

    void OnObjectCliked(ActionEventData displayData, ButtonDisplayHandler clickedButton)
    {
        if (currentObjectClickedRoutine != null)
        {
            StopCoroutine(currentObjectClickedRoutine);
        }
        currentObjectClickedRoutine = StartCoroutine(ObjectClickedRoutine(displayData, clickedButton));
    }
    
    public VariableTimeHandler variableTimeInterface;
    /**
     * Mutates the minutesTaken variable of displayData based on user input. 
     * Game will continue to run during the request. To pause any actions within a coroutine, 
     * use yield return new WaitWhile(() => displayData.minutesTaken == 0);
     */
    void RequestNewTime(ActionEventData displayData) {
        variableTimeInterface.InitializeTimeRequest(displayData);
    }

    Coroutine currentObjectClickedRoutine;
    /**
     * Coroutine used to add data to the timeline data object and add a graphical 
     * indicator to the game screen. 
     */
    IEnumerator ObjectClickedRoutine(ActionEventData actionEvent, ButtonDisplayHandler clickedButton)
    {
        // If the event takes "0 minutes", then it is actually of variable time. 
        if (actionEvent.isVariable) {
            RequestNewTime(actionEvent); // Mutates the minutesTaken variable in actionEvent based on user input
            yield return new WaitWhile(() => actionEvent.minutesTaken == 0);
        }

        // Add ActionEvent to the timeline
        bool succesfullyAdded = timelineData.AddActionEvent(actionEvent, timelineData.currentIndex);
        if (succesfullyAdded) // Generate a bar representing the new data.
        {
            GameObject barImageInstance = Instantiate(barImageObject, transform.GetChild(0));
            barImageInstance.GetComponent<MeterItemHandler>().InitializeValues(actionEvent, clickedButton);
            // The visible bar should be linked to the data item so that when the bar is removed, so is the data item. 
            barImageInstance.transform.SetSiblingIndex(timelineData.currentIndex);
            if (timelineData.currentIndex == timelineData.eventsInSequence.Count - 1) timelineData.currentIndex++;

            // deactivate the button, so that there can't be duplicates. 
            clickedButton.myButton.interactable = false;
            EventsManager.current.OnTimelineChanged();
        }
        else
        {
            print("Meter update failed");
        }


        yield return null;
    }

    public TimelineData timelineData;

    public void ClearTimeline()
    {
        MeterItemHandler[] meterItems = GetComponentsInChildren<MeterItemHandler>();
        for(int i = meterItems.Length - 1; i >= 0; i-- )
        {
            meterItems[i].DestroyMeterItem();
        }
    }

    public void CreateSleepBar(int timeToWakeUp)
    {
        GameObject barImageInstance = Instantiate(barImageObject, transform.GetChild(0));
        ActionEventData falseData = new ActionEventData
        {
            actionEnum = ActionEnum.GOTO,
            color = Color.gray,
            effect = null,
            isVariable = false,
            minutesTaken = timeToWakeUp,
            nameText = "sleep",
            scene = ""
        };
        bool succesfullyAdded = timelineData.AddActionEvent(falseData, 0);
        barImageInstance.GetComponent<MeterItemHandler>().InitializeValues(falseData, null);
        barImageInstance.GetComponent<MeterItemHandler>().destroyable = false;
        barImageInstance.transform.SetSiblingIndex(0);
    }

    public void ProtectAllBars()
    {
        MeterItemHandler[] meterItems = GetComponentsInChildren<MeterItemHandler>();
        for (int i = meterItems.Length - 1; i >= 0; i--)
        {
            meterItems[i].destroyable = false;
        }
    }
}
