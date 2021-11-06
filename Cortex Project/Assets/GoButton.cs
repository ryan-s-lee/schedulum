

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoButton : MonoBehaviour
{
    [SerializeField]
    Image darkenerImage;
    Button button;

    private void Awake()
    {
        Transform darkenerCanvas = GameObject.Find("DarknessCanvas").transform;
        Transform darkenerImageTransform = darkenerCanvas.GetChild(0);
        darkenerImage = darkenerImageTransform.GetComponent<Image>();
    }
    private void Start()
    {
        EventsManager.current.onTimelineChanged += OnTimelineChanged;
        SceneManager.activeSceneChanged += OnSceneChanged;

        button = GetComponent<Button>();
        button.interactable = false;
    }

    private void OnDestroy()
    {
        EventsManager.current.onTimelineChanged -= OnTimelineChanged;
        SceneManager.activeSceneChanged -= OnSceneChanged;
    }
    public void TriggerButton()
    {
        SoundManager.current.Click();
        StartCoroutine(FadeOut(1f, 1f));
    }

    void OnSceneChanged(Scene oldScene, Scene newScene)
    {
        if (newScene.name != "Scheduler" && newScene.name != "TutorialScheduler")
        {
            button.interactable = false;
        } else
        {
            Transform darkenerCanvas = GameObject.Find("DarknessCanvas").transform;
            Transform darkenerImageTransform = darkenerCanvas.GetChild(0);
            darkenerImage = darkenerImageTransform.GetComponent<Image>();
        }
    }

    public void OnTimelineChanged()
    {
        TimelineData timelineData = GameManager.current.timelineHandler.timelineData;
        if (timelineData.totalMinutes >= 960)
        {
            
            foreach(ActionEventData actionEvent in timelineData.eventsInSequence)
            {
                if (actionEvent.actionEnum == ActionEnum.GOTO && actionEvent.nameText == "Work")
                {
                    button.interactable = true;
                    return;
                }
            }
        }
        button.interactable = false;
    }

    IEnumerator FadeOut(float duration, float finalAlpha)
    {
        WaitForEndOfFrame frameWaiter = new WaitForEndOfFrame();

        Color oldColor = darkenerImage.color;
        Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, finalAlpha);
        float increment = Time.deltaTime / duration;
        for (float t = 0; t <= 1; t += increment)
        {
            darkenerImage.color = Color.Lerp(oldColor, newColor, t);
            yield return frameWaiter;
        }

        darkenerImage.color = newColor;
        yield return null;
        yield return null;
        GameManager.current.BeginDay();
    }
}
