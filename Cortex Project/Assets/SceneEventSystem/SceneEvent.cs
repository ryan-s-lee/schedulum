using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface SceneEvent
{
    void execute();

    void ExecuteNextNode();
}

public class SceneEventBase : SceneEvent
{
    
    public SceneEvent[] nextEvents;
    public int eventToTransitionTo;

    public virtual void execute()
    {

    }

    public void ExecuteNextNode()
    {
        nextEvents[eventToTransitionTo].execute();
    }
}

public class TalkSceneEvent : SceneEventBase
{
    public string speech = "";
    public Vector3 bubblePosition;
    public override void execute()
    {
        SceneEventManager.current.speechBubbleTransform.anchoredPosition = bubblePosition;
        SceneEventManager.current.bubbleText.text = speech;

    }
}