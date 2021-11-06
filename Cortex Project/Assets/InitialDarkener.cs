using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitialDarkener : MonoBehaviour
{
    Image darkenerImage;

    // Start is called before the first frame update
    void Start()
    {
        darkenerImage = transform.GetChild(0).GetComponent<Image>();
        darkenerImage.color = new Color(darkenerImage.color.r, darkenerImage.color.g, darkenerImage.color.b, 1f);
        StartCoroutine(FadeOut(2f, 0f));
    }

    // Update is called once per frame
    void Update()
    {
        
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
        yield return null;
        yield return null;
    }
}
