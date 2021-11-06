using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class SpeechBubbleHandler : MonoBehaviour
{
    Image speechBubbleImage;
    Text speechBubbleText;
    // Start is called before the first frame update
    void Start()
    {
        speechBubbleImage = transform.GetChild(0).GetComponent<Image>();
        speechBubbleText = transform.GetChild(0).GetChild(0).GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [YarnCommand("activatedarktext")]
    public void ActivateDarkText()
    {
        print("Is Running");
        speechBubbleImage.color = Color.black;
        speechBubbleText.color = Color.white;
    }

    [YarnCommand("activatelighttext")]
    public void ActivateLightText()
    {
        speechBubbleImage.color = Color.white;
        speechBubbleText.color = Color.black;
    }
}
