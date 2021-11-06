using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Compiler;

public class StatusMenuHandler : MonoBehaviour
{
    public static StatusMenuHandler current;
    [SerializeField]
    GameObject statusInfoPrefab;
    [SerializeField]
    Transform statusInfoContentContainer;
    public Transform statusDescriptionHoverText;

    private void Awake()
    {
        // Another singleton; there can only be one status menu, ever, and it should never be destroyed. 
        if (current == null)
        {
            current = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        statusDescriptionHoverText.gameObject.SetActive(false);
    }

    public StatusInfoObjectHandler AddStatusInfoObject(string name, string description)
    {
        StatusInfoObjectHandler statusInfoObject = Instantiate(statusInfoPrefab, statusInfoContentContainer).GetComponent<StatusInfoObjectHandler>();
        statusInfoObject.transform.GetComponent<Text>().text = name;
        statusInfoObject.description = description;
        return statusInfoObject;
    }
}
