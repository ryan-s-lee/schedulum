using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Yarn;
using Yarn.Compiler;

public class StatusEffect
{
    Func<bool> activationCondition;
    Func<bool> deactivationCondition;
    public string name;
    public string description;
    Action statusFunction;
    public StatusInfoObjectHandler statusInfoObject { get; set; }

    public StatusEffect()
    {
        this.activationCondition = () => { return false; };
        this.deactivationCondition = () => { return false; };
        this.name = "No Name";
        this.description = "No description";
        this.statusFunction = () => { };
    }
    public StatusEffect(string name, Func<bool> activationCondition, Func<bool> deactivationCondition, Action statusFunction)
    {
        this.activationCondition = activationCondition;
        this.deactivationCondition = deactivationCondition;
        this.name = name;
        this.statusFunction = statusFunction;
    }

    public bool ToBeActivated()
    {
        return activationCondition();
    }

    public bool ToBeDeactivated()
    {
        return deactivationCondition();
    }

    public void SetActivationCondition(Func<bool> activationCond)
    {
        activationCondition = activationCond;
    }

    public void SetDeactivationCondition(Func<bool> deactivationCond)
    {
        deactivationCondition = deactivationCond;
    }

    public void TriggerStatusEffect()
    {
        statusFunction();
    }

    public void SetStatusEffect(Action behavior)
    {
        statusFunction = behavior;
    }

    public void SetInfoObjectActive(bool isactive)
    {
        bool originallyActive = statusInfoObject.gameObject.activeInHierarchy;
        statusInfoObject.gameObject.SetActive(isactive);
        if (!originallyActive && isactive)
        {
            statusInfoObject.SetToUnread();
        }
    }
}