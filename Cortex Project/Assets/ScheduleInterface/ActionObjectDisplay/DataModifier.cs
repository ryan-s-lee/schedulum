using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataModifier : MonoBehaviour
{

    public Text nameText;
    public Text timeText;
    public Image objectSprite;
    // Start is called before the first frame update
    void Start()
    {
        EventsManager.current.onObjectHover += ModifyDisplay;
    }

    private void OnDestroy()
    {
        EventsManager.current.onObjectHover -= ModifyDisplay;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ModifyDisplay(string nameText, string timeText, Sprite sprite)
    {
        
        this.nameText.text = nameText;
        this.timeText.text = timeText;
        this.objectSprite.sprite = sprite;
    }
}
