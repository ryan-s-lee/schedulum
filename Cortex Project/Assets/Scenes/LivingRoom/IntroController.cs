using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{
    public string scenename;
    public void LoadScheduler()
    {
        SceneManager.LoadScene(scenename);
    }

    public void ResetData()
    {
        if (GameDataSingleton.current != null)
        {
            Destroy(GameDataSingleton.current.gameObject);
            Destroy(GameManager.current.gameObject); // destroys the GameManager and EventsManager
            GameManager.current = null;
            EventsManager.current.ClearEvents();
            EventsManager.current = null;
            Destroy(StatusMenuHandler.current.gameObject);
            StatusMenuHandler.current = null;
        }
    }
}
