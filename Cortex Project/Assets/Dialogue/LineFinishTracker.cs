using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Timeline;
using UnityEngine.UI;
using Yarn.Unity;

public class LineFinishTracker : MonoBehaviour
{
    public GameObject spaceIndicator;
    public DialogueUI dialogueUI;
    public static KeyCode activationKey = KeyCode.Space;
    WaitUntil waitUntilActivationKeyPressed = new WaitUntil(() => Input.GetKeyDown(activationKey));

    Coroutine currentListener;

    public void ListenForActivation()
    {
        currentListener = StartCoroutine(ActivationCoroutine());
    }

    IEnumerator ActivationCoroutine()
    {
        spaceIndicator.SetActive(true);
        yield return waitUntilActivationKeyPressed;
        dialogueUI.MarkLineComplete();
        yield return null;
        spaceIndicator.SetActive(false);
    }

    public void SelectObjectWithDelay(GameObject obj)
    {
        StartCoroutine(SelectCoroutine(obj));
    }

    IEnumerator SelectCoroutine(GameObject obj)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(obj);
        yield return null;

    }
}
