using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonDisplayHandler : MonoBehaviour, IPointerEnterHandler
{
    public Image spriteThing;

    public int minutesTaken;
    
    public ActionEnum action;
    [HideInInspector]
    public Button myButton;

    public string scene;
    ActionEventData data = new ActionEventData();

    public void Start()
    {
        myButton = GetComponent<Button>();
        if (myButton != null)
        {
            data.actionEnum = action;
            data.nameText = name;
            data.minutesTaken = minutesTaken;
            data.sprite = spriteThing.sprite;
            data.isVariable = minutesTaken == 0;
            data.scene = scene;
            myButton.onClick.AddListener(() => GenerateMeterPipOnClick());
            myButton.onClick.AddListener(() => SoundManager.current.Click());
        }
    }

    void GenerateMeterPipOnClick()
    {
        data.color = Random.ColorHSV(0, 1, 0, 1, 1, 1, 1, 1);
        EventsManager.current.ObjectClick(data, this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (minutesTaken == 0)
        {
            EventsManager.current.ObjectHover(action.ToString() + " " + name, "variable", spriteThing.sprite);
        } else
        {
            int hours = minutesTaken/60;
            int remainingMins = minutesTaken % 60;
            EventsManager.current.ObjectHover(action.ToString() + " " + name, hours + " hrs " + remainingMins + " min", spriteThing.sprite);
            // EventsManager.current.ObjectHover(action.ToString() + " " + name, minutesTaken + " min", spriteThing.sprite);
        }
    }

    public void ResetData()
    {
        if (data.isVariable)
        {
            data.minutesTaken = 0;
        }
    }
}
