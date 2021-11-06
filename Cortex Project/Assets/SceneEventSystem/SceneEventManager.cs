using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// You should never actually use this class
public class SceneEventManager : MonoBehaviour
{
    public static SceneEventManager current;
    public RectTransform speechBubbleTransform;
    public RectTransform responsesTransform;
    public Text bubbleText;

    // Start is called before the first frame update
    private void Awake()
    {
        current = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
