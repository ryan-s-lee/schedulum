using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreenHandler : MonoBehaviour
{
    Image darkenerImage;
    // Start is called before the first frame update
    void Start()
    {
        darkenerImage = transform.Find("DarknessCanvas").GetChild(0).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SoundManager.current.Click();
        StartCoroutine(FadeOut(1, 1.0f));
    }

    IEnumerator FadeOut(float duration, float finalAlpha)
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
        SceneManager.LoadScene("LivingRoom");
    }
}
