using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateVariableTimeDisplay : MonoBehaviour
{
    public Text variableTimeText;
    public void UpdateText(int val) {
        int hours = val / 60;
        int min = val % 60;
        variableTimeText.text = hours + " hrs " + min + " min";
    }
    
    public void UpdateColor(Color color) {
        variableTimeText.color = color;
    }
}
