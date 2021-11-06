using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneRef
{
    static Dictionary<string, string> sceneRef = new Dictionary<string, string>()
    {
        
    };

    public static string FindScene(string name)
    {
        return sceneRef[name];
    }
}
