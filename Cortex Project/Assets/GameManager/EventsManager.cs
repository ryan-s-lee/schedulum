using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class EventsManager : MonoBehaviour
{
    public static EventsManager current;
    public event Action<string, string, Sprite> onObjectHover;
    public event Action<ActionEventData, ButtonDisplayHandler> onObjectClick;
    public event Action<int> onEventItemDestroyed;
    public event Action onTimelineChanged;
    public event Action<PlayerData> onStatsChanged;
    public event Action onDayFinish;
    public event Action<ActionEventData> onEventFinish;
    public event Action onAnyButtonClicked;

    public void ClearEvents()
    {
        if (onObjectHover != null)
        {
            foreach (Action<string, string, Sprite> d in onObjectHover.GetInvocationList())
            {
                onObjectHover -= d;
            }
        }
        if (onObjectClick != null)
        {
            foreach (Action<ActionEventData, ButtonDisplayHandler> d in onObjectClick.GetInvocationList())
            {
                onObjectClick -= d;
            }
        }
        if (onEventItemDestroyed != null) { foreach (Action<int> d in onEventItemDestroyed.GetInvocationList())
            {
                onEventItemDestroyed -= d;
            }
        }
        if (onStatsChanged != null)
        {
            foreach (Action<PlayerData> d in onStatsChanged.GetInvocationList())
            {
                onStatsChanged -= d;
            }
        }
    }

    private void Awake()
    {
        if (current == null) { current = this; } // no need to destroy gameobjects because 
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ObjectHover(String nameText, String timeText, Sprite sprite) { if (onObjectHover != null) { onObjectHover(nameText, timeText, sprite); } }
    public void ObjectClick(ActionEventData displayData, ButtonDisplayHandler clickedButton) { if (onObjectClick != null) { onObjectClick(displayData, clickedButton); onTimelineChanged(); } }
    public void EventItemDestroyed(int orderOfItem) { if (onEventItemDestroyed != null) { onEventItemDestroyed(orderOfItem); onTimelineChanged(); } }
    public void StatsChanged(PlayerData playerData){ if (onStatsChanged != null ) { onStatsChanged(playerData); } }

    [YarnCommand("announceEventFinish")] // So that at the end of every dialogue we can call this easily
    public void EventFinish(ActionEventData eventData) { if (onEventFinish != null) { onEventFinish(eventData); } }
    public void DayFinish() { if (onDayFinish != null) { onDayFinish(); } }

    public void OnTimelineChanged() { if (onTimelineChanged != null) onTimelineChanged(); }

    public void OnAnyButtonClicked() { if (onAnyButtonClicked != null) onAnyButtonClicked(); }
    
}
