using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Yarn;
using Yarn.Unity;

public class CustomVariableStorage : InMemoryVariableStorage
{
    
    public override Value GetValue(string variableName)
    {
        if (variableName.StartsWith("$status_")) // 8 characters, indexes 0-7, so actualstatus starts on character 8
        {
            // remember that all status effects names are lowercase. Replace spaces with underscores
            // Missed capitalization forgiveness. 
            return new Value(GameManager.current.playerData.StatusIsActive(variableName.Substring(8).ToLower().Replace("_", " ")));
        } else if (variableName.StartsWith("$did_")) // 5 characters, indexes 0-4, so actual event name starts on char 5
        {
            
            string daysString = variableName.Substring(variableName.LastIndexOf("_"));
            string actualEventName = variableName.Substring(5);
            actualEventName = actualEventName.Substring(0, actualEventName.Length - daysString.Length).Replace("_", " ");
            // format should be, after [event_name]_[numdays]
            return new Value(Calendar.current.GetNumOfEventsWithinTime(actualEventName, int.Parse(daysString.Substring(1))));
        }
        switch (variableName)
        {
            case "$fitness":
                return new Value(GameManager.current.playerData.fitness);
            case "$happiness":
                return new Value(GameManager.current.playerData.happiness);
            case "$hunger":
                return new Value(GameManager.current.playerData.hunger);
            case "$wealth":
                return new Value(GameManager.current.playerData.wealth);
            case "$will":
                return new Value(GameManager.current.playerData.will);
            case "$time":
                return new Value(GetTime());
            case "$formattedtime":
                return new Value(GetFormattedTimeString());
            case "$wakeuptime":
                return new Value(GameManager.current.timeToWakeUp);
            case "$_eventduration":
                return new Value(GameManager.current.currentEvent.minutesTaken);
            case "$_randomfloat":
                return new Value(UnityEngine.Random.value);
            case "$_objectname":
                if (variables.ContainsKey("$_objectname")) { return variables["$_objectname"]; } // We can set the variable ourselves for impulse actions. 
                return new Value(GameManager.current.currentEvent.nameText);
            case "$_actionname":
                return new Value(GameManager.current.currentEvent.actionEnum.ToString());
            default:
                return base.GetValue(variableName);
        }
    }

    public override void ResetToDefaults()
    {
        base.ResetToDefaults();
    }

    public override void SetValue(string variableName, Value value)
    {
        print(variableName + " set to " + value.AsString);
        float val = value.AsNumber;
        switch (variableName)
        {
            case "$fitness":
                GameManager.current.playerData.fitness = val;
                break;
            case "$happiness":
                GameManager.current.playerData.happiness = val;
                break;
            case "$hunger":
                GameManager.current.playerData.hunger = val;
                break;
            case "$wealth":
                GameManager.current.playerData.wealth = val;
                break;
            case "$will":
                GameManager.current.playerData.will = val;
                break;
            case "$wakeuptime":
                GameManager.current.timeToWakeUp = (int)val;
                break;
            default:
                base.SetValue(variableName, value);
                break;
        }
    }

    string GetFormattedTimeString()
    {
        int totalMinutesPassed = GetTime();
        // Since the day starts at 5, we can add 5 hours' worth of minutes. 
        totalMinutesPassed += 5 * 60;
        string ampm = "AM";
        TimeSpan hoursAndMinutes = TimeSpan.FromMinutes(totalMinutesPassed);
        int hours = hoursAndMinutes.Hours;
        int minutes = hoursAndMinutes.Minutes;
        if (hours > 12)
        {
            ampm = "PM";
            hours -= 12;
        } else if (hours == 12)
        {
            ampm = "PM";
        }

        string hoursandmin = hours + ":" + minutes;
        if (minutes < 10) { hoursandmin += "0"; }
        return hoursandmin + " " + ampm;
    }

    int GetTime()
    {
        int totalMinutesPassed = 0;
        int currentEventIndex = GameManager.current.timelineHandler.timelineData.currentIndex;
        for (int i = 0; i < currentEventIndex; i++)
        {
            totalMinutesPassed += GameManager.current.timelineHandler.timelineData.eventsInSequence[i].minutesTaken;
        }
        
        return totalMinutesPassed;
    }
}
