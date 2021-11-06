using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableTimeHandler : MonoBehaviour
{
    private void Start()
    {
        GameManager.current.timelineHandler.variableTimeInterface = this;
        gameObject.SetActive(false);
    }

    public UpdateVariableTimeDisplay timeDisplay;
    int currentTime;
    public Color unavailableTimeColor;
    public Color availableTimeColor;

    public void UpdateCurrentTime(int changeVal) {
        SoundManager.current.Click();

        currentTime += changeVal;
        
        if (!TimeValid(currentTime)) {
            timeDisplay.UpdateColor(unavailableTimeColor);
        } else {
            timeDisplay.UpdateColor(availableTimeColor);
        }
        
        timeDisplay.UpdateText(currentTime);
    }
    private ActionEventData currentDisplayData;

    public void SetCurrentTime(int newVal) {
        timeDisplay.UpdateText(newVal);
        if (!TimeValid(newVal)) {
            timeDisplay.UpdateColor(unavailableTimeColor);
        } else {
            timeDisplay.UpdateColor(availableTimeColor);
        }
        currentTime = newVal;
    }

    /**
     * Method called by other classes. Initializes the user interface for inputting a variabletime. 
     */
    public void InitializeTimeRequest(ActionEventData displayData)
    {
        gameObject.SetActive(true);
        SetCurrentTime(0);
        currentDisplayData = displayData;
    }

    public void CancelTimeRequest()
    {
        SoundManager.current.Click();
        gameObject.SetActive(false);
    }
    public void SubmitTimeRequest()
    {
        if (TimeValid(currentTime))
        {
            currentDisplayData.minutesTaken = currentTime;
            gameObject.SetActive(false);
            SoundManager.current.Click();
        }
    }

    bool TimeValid(int time)
    {
        return time <= GameManager.current.timelineHandler.timelineData.minutesLeftInDay && time > 0;
    }
}
