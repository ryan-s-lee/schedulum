using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class DialogueParticipantsTracker : MonoBehaviour
{
    public Transform speechBubbleObject;
    public Transform responseObject;
    public List<Transform> objectsToTrack;

    [YarnCommand("setdialoguepos")]
    public void SetTextPos(string objectName)
    {
        foreach (Transform obj in objectsToTrack)
        {
            if(obj.name.Equals(objectName))
            {
                speechBubbleObject.position = obj.position;
                return;
            }
        }
    }

    [YarnCommand("print")]
    public void CustomPrint(string str)
    {
        print(str);
    }

    [YarnCommand("setresponsepos")]
    public void SetResponsePos(string objectName)
    {
        foreach (Transform obj in objectsToTrack)
        {
            if (obj.name.Equals(objectName))
            {
                responseObject.position = obj.position;
                return;
            }
        }
    }
}
