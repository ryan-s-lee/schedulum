using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/**
     * Data class containing a list of ActionEvents, 
     * which contain information on Events that the player 
     * has selected to happen throughout the day. 
     */
[Serializable]
public class TimelineData
{
    public int currentIndex;
    private int _totalMinutes;
    public int totalMinutes
    {
        get { return _totalMinutes; }
    }

    public int minutesLeftInDay
    {
        get { return maxMinutes - _totalMinutes; }
    }
    public int maxMinutes;
    public List<ActionEventData> eventsInSequence = new List<ActionEventData>();
    public bool AddActionEvent(ActionEventData actionEvent, int orderOfAction)
    {
        if (totalMinutes + actionEvent.minutesTaken <= maxMinutes)
        {
            eventsInSequence.Insert(orderOfAction, actionEvent);
            _totalMinutes += actionEvent.minutesTaken;
            return true;
        }

        return false;
    }

    public void RemoveActionEvent(int orderOfAction)
    {
        if (orderOfAction >= 0 && orderOfAction < eventsInSequence.Count)
        {
            _totalMinutes -= eventsInSequence[orderOfAction].minutesTaken;
            eventsInSequence.RemoveAt(orderOfAction);
            if (currentIndex > orderOfAction) currentIndex--;
        }
    }
}