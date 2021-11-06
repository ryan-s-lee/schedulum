using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Calendar : MonoBehaviour
{
    public static Calendar current;
    List<List<string>> calendar;

    private void Awake()
    {
        if (current == null)
        {
            current = this;
            calendar = new List<List<string>>();
            // Start with a list to track events of the first day
            calendar.Add(new List<string>());
        } // no need for else because the Calendar object will automatically be deleted by the GameManager. 
    }
    // Start is called before the first frame update
    void Start()
    {
        EventsManager.current.onDayFinish += OnDayFinish;
    }

    void OnDayFinish()
    {
        while (calendar.Count >= 30)
        {
            calendar.RemoveAt(0);
        }
        calendar.Add(new List<string>()); // set up the new day
    }

    public int GetNumOfEventsWithinTime(string eventName, int days)
    {
        int count = 0;
        // Check each day until the number of days before the current day or until there are no more days to check. 
        for(int i = calendar.Count - 1; i > calendar.Count - 1 - days && i >= 0; i--)
        {
            // Check the events that happened on the day currently being checked.
            foreach(string eventInDay in calendar[i]) {
                if (eventInDay.Equals(eventName)) { count++; }
            }
        }

        return count;
    }

    public void AddEventToCalendar(string str)
    {
        calendar[calendar.Count - 1].Add(str);
    }

    public string GetMostRecentEvent()
    {
        List<string> dayCalendar = calendar[calendar.Count - 1];
        return dayCalendar.Count == 0 ? "" : dayCalendar[dayCalendar.Count - 1];
    }
    public string GetEventSinceMostRecent(int numEvents)
    {
        List<string> dayCalendar = calendar[calendar.Count - 1];
        return dayCalendar.Count == 0 ? "" : dayCalendar[dayCalendar.Count - 1 - numEvents];
    }
}
