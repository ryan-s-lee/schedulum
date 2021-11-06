using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class GameManager : MonoBehaviour
{
    public static GameManager current;
    public TimelineHandler timelineHandler;

    public int timeToWakeUp;

    //public class ActionEventData
    //{
    //    public ActionEnum actionEnum;
    //    public string nameText;
    //    public int minutesTaken;
    //    public Color color;
    //    public Sprite sprite;
    //    public bool isVariable;
    //    public string scene;
    //    public Effect effect;
    //}
    public ActionEventData currentEvent
    {
        get
        {
            if (timelineHandler != null)
            {
                int currentIndex = timelineHandler.timelineData.currentIndex;
                if (currentIndex == timelineHandler.timelineData.eventsInSequence.Count)
                {
                    // Give the sleeping ActionEvent
                    return new ActionEventData
                    {
                        actionEnum = ActionEnum.GOTO,
                        nameText = "Sleep",
                        minutesTaken = 0, // doesn't matter because happiness and will gains in Yarn don't depend on _eventduration
                        color = Color.white,
                        sprite = null,
                        isVariable = false,
                        scene = "Sleeping",
                        effect = null // Will delete eventually
                    };
                } else
                {
                    return timelineHandler.timelineData.eventsInSequence[currentIndex];
                }
            } else
            {
                print("no timeline available");
                return null;
            }
        }
    }

    public StatusMenuHandler statusMenu;

    public bool gameIsOnLastEvent
    {
        get
        {
            int currentIndex = timelineHandler.timelineData.currentIndex;
            int lastIndex = timelineHandler.timelineData.eventsInSequence.Count - 1;
            return currentIndex == lastIndex;
        }
    }

    public PlayerData playerData;

    void Awake()
    {
        if (current == null) {
            current = this;
            EventsManager.current = GetComponent<EventsManager>();
            playerData = new PlayerData()
            {
                fitness = 70,
                happiness = 80,
                wealth = 300,
                hunger = 40,
                will = 100,
                inventory = new Dictionary<string, int>() // Not really necessary
            };
            EventsManager.current.onEventFinish += OnEventFinish;
            EventsManager.current.onDayFinish += OnDayFinish;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }
    }
    // Start is called before the first frame update
    void Start()
    {
        // By default, wake up at 7:00 AM. 
        timeToWakeUp = 120;
        dayIsPlaying = false;
        statusMenu.gameObject.SetActive(false);
        StatusHelper.InitializeStatus();
        Invoke("InitialStatChanged", 0f);
        UpdateStatuses();
    }
    void InitialStatChanged()
    {
        EventsManager.current.StatsChanged(playerData);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // toggle statusmenu
            statusMenu.gameObject.SetActive(!statusMenu.gameObject.activeInHierarchy);
        }
    }

    private void OnDestroy()
    {
        if (current == this)
        {
            EventsManager.current = null; // Because EventsManager is part of GameManager
            current = null;
        }
    }

    public void OnEventFinish(ActionEventData actionEvent)
    {
        // UpdateStats(actionEvent);

        // We run AddEventToCalendar first, so that we can keep track of the current event while we update stats. 
        Calendar.current.AddEventToCalendar(currentEvent.actionEnum.ToString() + currentEvent.nameText);

        RunActiveStatuses();
        UpdateStatuses();
        if (playerData.fitness < 10 || playerData.will < 10 || playerData.hunger < 10) // conditions for hospitalization
        {
            SceneManager.LoadScene("Hospital");
        } else if (playerData.wealth >= 3000)
        {
            SceneManager.LoadScene("End");
        }
        else
        {
            timelineHandler.timelineData.currentIndex++;
            if (timelineHandler.timelineData.currentIndex < timelineHandler.timelineData.eventsInSequence.Count)
            {
                SceneManager.LoadScene(timelineHandler.timelineData.eventsInSequence[timelineHandler.timelineData.currentIndex].scene);
            }
            else if (timelineHandler.timelineData.currentIndex == timelineHandler.timelineData.eventsInSequence.Count)
            {
                SceneManager.LoadScene("Sleeping");
            }
            else
            {
                EventsManager.current.DayFinish();
            }
        }
    }

    public bool dayIsPlaying;
    public void OnDayFinish()
    {
        dayIsPlaying = false;
        timelineHandler.ClearTimeline();
        playerData.timelineData.currentIndex = 0;
        SceneManager.LoadScene("Scheduler");
        timelineHandler.CreateSleepBar(timeToWakeUp);
    }
    

    void RunActiveStatuses()
    {
        foreach (StatusEffect status in playerData.activeStatuses)
        {
            status.TriggerStatusEffect();
        }
    }

    public void UpdateStatuses()
    {
        // make lists of indices to keep track of what statuses are to be removed from which list
        List<int> activeStatusEffectsToDeactivate = new List<int>();
        List<int> inactiveStatusEffectsToActivate = new List<int>();

        // Find the active statuses to be deactivated
        for (int i = playerData.activeStatuses.Count - 1; i >= 0; i--)
        {
            if (playerData.activeStatuses[i].ToBeDeactivated())
            {
                activeStatusEffectsToDeactivate.Add(i);
            }
        }

        // Find the inactive statuses to be activated
        for (int i = playerData.inactiveStatuses.Count - 1; i >= 0; i--)
        {
            if (playerData.inactiveStatuses[i].ToBeActivated())
            {
                inactiveStatusEffectsToActivate.Add(i);
            }
        }

        // Since list of status indices to swap is already in reverse, we don't have 
        // to worry about iterating in reverse. 
        foreach (int i in activeStatusEffectsToDeactivate) 
        {
            playerData.activeStatuses[i].SetInfoObjectActive(false);
            playerData.inactiveStatuses.Add(playerData.activeStatuses[i]);
            playerData.activeStatuses.RemoveAt(i);
        }
        foreach (int i in inactiveStatusEffectsToActivate) // iterate in reverse to ensure correctness
        {
            playerData.inactiveStatuses[i].SetInfoObjectActive(true);
            playerData.activeStatuses.Add(playerData.inactiveStatuses[i]);
            playerData.inactiveStatuses.RemoveAt(i);
        }
    }

    public void BeginDay()
    {
        if (timelineHandler != null)
        {
            // Obtain the timeline data, which we will read off of in order to determine
            // the buffs that the players get. 
            playerData.timelineData = timelineHandler.timelineData;
        }
        dayIsPlaying = true;
        playerData.timelineData.currentIndex = 1;
        SceneManager.LoadScene(timelineHandler.timelineData.eventsInSequence[1].scene);
    }


    // YARN COMMANDS
    //fitness = 100,
    //happiness = 100,
    //wealth = 100,
    //hunger = 100,
    //will = 100,
    [YarnCommand("changestat")]
    public void ChangeStat(string stat, string input)
    {
        int val = int.Parse(input);
        switch (stat)
        {
            case "fitness":
                playerData.fitness += val;
                break;
            case "happiness":
                playerData.happiness += val;
                break;
            case "hunger":
                playerData.hunger += val;
                break;
            case "wealth":
                playerData.wealth += val;
                break;
            case "will":
                playerData.will += val;
                break;
        }
    }

    [YarnCommand("print")]
    public void printToConsole(string msg)
    {
        print(msg);
    }
}
