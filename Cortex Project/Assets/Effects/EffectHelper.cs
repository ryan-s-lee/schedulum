using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class EffectHelper : MonoBehaviour
{
    public List<Effect> possibleEffects;
    public static Dictionary<string, Effect> effectsDict;

    private void Awake()
    {
        if (effectsDict != null) return;

        effectsDict = new Dictionary<string, Effect>();

        foreach (Effect effect in possibleEffects)
            effectsDict.Add(effect.name, effect);
    }

    Effect CopyEffect(Effect effectToCopy)
    {
        Effect newEffect = new Effect();
        newEffect.wealthChangePerTimeUnit = effectToCopy.wealthChangePerTimeUnit;
        newEffect.wealthChangeTimeUnit = effectToCopy.wealthChangeTimeUnit;
        newEffect.willChange = effectToCopy.willChange;
        newEffect.willChangePerTimeUnit = effectToCopy.willChangePerTimeUnit;
        newEffect.willChangeTimeUnit = effectToCopy.willChangeTimeUnit;
        newEffect.hungerChange = effectToCopy.hungerChange;
        newEffect.hungerChangePerTimeUnit = effectToCopy.hungerChangePerTimeUnit;
        newEffect.hungerChangeTimeUnit = effectToCopy.hungerChangeTimeUnit;
        newEffect.happinessChange = effectToCopy.happinessChange;
        newEffect.happinessChangePerTimeUnit = effectToCopy.happinessChangePerTimeUnit;
        newEffect.happinessChangeTimeUnit = effectToCopy.happinessChangeTimeUnit;
        newEffect.fitnessChange = effectToCopy.fitnessChange;
        newEffect.fitnessChangePerTimeUnit = effectToCopy.fitnessChangePerTimeUnit;
        newEffect.fitnessChangeTimeUnit = effectToCopy.fitnessChangeTimeUnit;

        newEffect.statusApplied = new List<string>(effectToCopy.statusApplied);
        newEffect.statusRemoved = new List<string>(effectToCopy.statusRemoved);

        return newEffect;
    }

    public static Effect FindEffect(string effectName)
    {
        bool success = effectsDict.TryGetValue(effectName, out Effect actualEffect);
        if (success)
        {
            return actualEffect;
        } else
        {
            return null;
        }
    }
}
