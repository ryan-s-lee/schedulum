using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StatusInfoObjectHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [HideInInspector]
    public string description;
    Transform speechBubble;
    Text speechBubbleText;
    public Text statusInfoText;

    void Start()
    {
        speechBubble = StatusMenuHandler.current.statusDescriptionHoverText;
        speechBubbleText = speechBubble.GetChild(0).GetChild(0).GetComponent<Text>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        statusInfoText.color = Color.white;
        speechBubble.gameObject.SetActive(true);
        speechBubble.position = transform.position + Vector3.up * 20f;
        speechBubbleText.text = description;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        speechBubble.gameObject.SetActive(false);
    }

    public void SetToUnread()
    {
        statusInfoText.color = Color.yellow;
    }
}
