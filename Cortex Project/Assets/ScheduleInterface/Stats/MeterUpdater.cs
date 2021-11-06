using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeterUpdater : MonoBehaviour
{
    public enum StatTypes { Happiness, Fitness, Hunger, Will, Wealth };
    public StatTypes statType;
    Image symbol;
    Image meterImage;
    Text meterText;
    int meterAmount;

    Shader shaderGUItext;
    Shader shaderSpritesDefault;
    // Start is called before the first frame update
    void Start()
    {
        EventsManager.current.onStatsChanged += UpdateMeter;
        Transform meterImageTrans = transform.Find("Meter");
        Transform meterTextTrans = transform.Find("Text");
        symbol = GetComponent<Image>();
        if (meterImageTrans != null)
        {
            meterImage = meterImageTrans.GetComponent<Image>();
        }
        if (meterTextTrans != null) { 
            meterText = meterTextTrans.GetComponent<Text>();
            if (statType != StatTypes.Wealth) { meterText.color = Color.white; }
        }

        // shaderGUItext = Shader.Find("GUI/Text Shader");
        shaderSpritesDefault = Shader.Find("UI/Default");
        symbol.material.shader = shaderSpritesDefault;

    }
    // I just like the entire album since it was general pretty good
    // But I put 3 of the pieces in my playlists
    // Donut County is a video game
    // So the music is typically gonna be 
    // The game is about making stuff fall into a hole
    // It's kinda stupid so ig the music kinda fits
    // Never heard of those two
    // I don't really like full albums anymore
    // All those albums, I a couple years ago
    // What I meant was that those pieces don't necessarily reflect my current tastes


    private void OnDestroy()
    {
        EventsManager.current.onStatsChanged -= UpdateMeter;
    }

    void UpdateMeter(PlayerData playerData)
    {
        switch(statType)
        {
            case StatTypes.Happiness:
                meterAmount = (int)playerData.happiness;
                break;
            case StatTypes.Fitness:
                meterAmount = (int)playerData.fitness;
                break;
            case StatTypes.Hunger:
                meterAmount = (int)playerData.hunger;
                break;
            case StatTypes.Will:
                meterAmount = (int)playerData.will;
                break;
            case StatTypes.Wealth:
                meterAmount = (int)playerData.wealth;
                meterText.text = meterAmount.ToString();
                return;
        }
        if (meterImage != null)
        {
            meterImage.fillAmount = meterAmount / 100f;
            if (meterAmount >= 90)
            {
                StopFlashing();
                meterImage.color = Color.cyan;
            } else if (meterAmount >= 50)
            {
                StopFlashing();

                meterImage.color = Color.green;
            } else if (meterAmount >= 10)
            {
                StopFlashing();
                meterImage.color = Color.yellow;
            } else
            {
                meterImage.color = Color.red;
                if (statType == StatTypes.Will || statType == StatTypes.Hunger || statType == StatTypes.Fitness) // These are the stats that can kill you
                {
                    if (currentlyFlashing)
                    {
                        StopCoroutine(currentFlasher);
                    }
                    currentlyFlashing = true;
                    currentFlasher = StartCoroutine(FlashRed());
                }
            }
        }
        if (meterText != null)
        {
            meterText.text = meterAmount.ToString() + "/100"; // all stats are out of 100 except for wealth, which is special-cased to not have the denominator. 
        }
    }

    void StopFlashing()
    {

        if (!currentlyFlashing) return;
        StopCoroutine(currentFlasher);
        symbol.color = Color.white;
        currentlyFlashing = false;
    }
    WaitForSeconds flashWaiter = new WaitForSeconds(0.65f);
    Coroutine currentFlasher;
    bool currentlyFlashing = false;
    IEnumerator FlashRed()
    {
        while (true)
        {
            symbol.color = Color.red;
            yield return flashWaiter;
            symbol.color = Color.white;
            yield return flashWaiter;
        }
    }
}
