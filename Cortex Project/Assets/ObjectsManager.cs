using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsManager : MonoBehaviour
{
    GameObject currentPanel;
    // Start is called before the first frame update
    void Start()
    {
        currentPanel = transform.GetChild(0).gameObject;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        currentPanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchPanel(GameObject panelToSwitchTo)
    {
        currentPanel.SetActive(false);
        panelToSwitchTo.SetActive(true);
        currentPanel = panelToSwitchTo;
        
    }
}
