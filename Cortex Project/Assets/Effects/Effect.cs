using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/EffectScriptableObject", order = 1)]
public class Effect : ScriptableObject
{
    [Header("Wealth")]
    public sbyte wealthChange;
    public sbyte wealthChangePerTimeUnit;
    public sbyte wealthChangeTimeUnit;
    // public byte inventoryChange;

    [Header("Will")]
    public sbyte willChange;
    public sbyte willChangePerTimeUnit;
    public sbyte willChangeTimeUnit;

    [Header("Hunger")]
    public sbyte hungerChange;
    public sbyte hungerChangePerTimeUnit;
    public sbyte hungerChangeTimeUnit;

    [Header("Happiness")]
    public sbyte happinessChange;
    public sbyte happinessChangePerTimeUnit;
    public sbyte happinessChangeTimeUnit;

    [Header("Fitness")]
    public sbyte fitnessChange;
    public sbyte fitnessChangePerTimeUnit;
    public sbyte fitnessChangeTimeUnit;

    [Header("Status")]
    public List<string> statusApplied;
    public List<string> statusRemoved;
}
