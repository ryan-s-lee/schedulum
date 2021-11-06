using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class DialogueUtilities : MonoBehaviour
{
    DialogueRunner dR;
    DialogueUI dI;
    public Image darkenerImage;
    // Start is called before the first frame update
    void Start()
    {
        dR = GetComponent<DialogueRunner>();
        //volume = Camera.main.transform.Find("Global Volume").GetComponent<Volume>();
        //profile = volume.profile;
        //// Initialize brightness to zero. 
        //if (profile.TryGet(out LiftGammaGain lgg)) {
        //    lgg.gain.value = new Vector4(lgg.gain.value.x, lgg.gain.value.y, lgg.gain.value.z, -1f);
        //}
        dI = GetComponent<DialogueUI>();
        dI.onLineStart.AddListener(() => SoundManager.current.SetTalkingMode(true));
        dI.onLineFinishDisplaying.AddListener(() => SoundManager.current.SetTalkingMode(false));

        if (darkenerImage == null)
        {
            darkenerImage = transform.parent.Find("DarknessCanvas").GetChild(0).GetComponent<Image>();
        }
        darkenerImage.color = new Color(darkenerImage.color.r, darkenerImage.color.g, darkenerImage.color.b, 1f);
    }

    public void TriggerEventFinish()
    {
        EventsManager.current.EventFinish(GameManager.current.currentEvent);
    }

    [YarnCommand("opencurtains")]
    public void OpenCurtains(string duration)
    {
        StartCoroutine(ChangeDarknessRoutine(float.Parse(duration), 0f));
    }

    IEnumerator ChangeDarknessRoutine(float duration, float finalAlpha)
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
    }

    [YarnCommand("closecurtains")]
    public void CloseCurtains(string duration)
    {
        StartCoroutine(ChangeDarknessRoutine(float.Parse(duration), 1f));
    }

    // Events should be capitalized, including Of, And, etc. (Bored Of Grapes Of Wrath)
    [YarnCommand("trackevent")]
    public void TrackEvent(string[] eventName)
    {
        string finalString = "";
        foreach (string eventWord in eventName)
        {
            string replaceStr = eventWord;
            if (eventWord.StartsWith("$")) {
                replaceStr = dR.variableStorage.GetValue(eventWord).AsString;
            }
            finalString += replaceStr + " ";
        }

        finalString = finalString.Trim();
        print("Tracked: " + finalString);
        Calendar.current.AddEventToCalendar(finalString);
    }
}
