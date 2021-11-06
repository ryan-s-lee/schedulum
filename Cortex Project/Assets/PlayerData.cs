using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Yarn.Compiler;
using Yarn.Unity;

public class PlayerData
{
    private float _hunger;
    public float hunger
    {
        get { return _hunger; }
        set
        {
            float changeMultiplier = 1;
            float change = value - _hunger;
            _hunger += change * changeMultiplier;
            _hunger = Mathf.Clamp(_hunger, 0, 110); // If the hunger > 100, apply the stuffed and indegestion status effect
            EventsManager.current.StatsChanged(this);
        }
    }

    private float _will;
    public float will
    {
        get { return _will; }
        set
        {
            float changeMultiplier = 1f;
            float change = value - _will;
            bool changeIsPos = change > 0;
            if (StatusIsActive("enlightened"))
            {
                if (!changeIsPos) { changeMultiplier -= 0.1f; }
            }
            if (StatusIsActive("elated"))
            {
                if (changeIsPos) { changeMultiplier += 0.2f; }
                else { changeMultiplier -= 0.1f; }
            }
            if (StatusIsActive("depressed"))
            {
                if (!changeIsPos) { changeMultiplier += 0.2f; }
                else { changeMultiplier -= 0.1f; }
            }
            if (StatusIsActive("overstuffed"))
            {
                if (!changeIsPos) { changeMultiplier += 0.1f; }
            }
            if (StatusIsActive("hungry"))
            {
                if (!changeIsPos) { changeMultiplier += 0.1f; }
            }
            _will += change * changeMultiplier;
            _will = Mathf.Clamp(_will, 0, 100);
            EventsManager.current.StatsChanged(this);
        }
    }

    private float _wealth;
    public float wealth
    {
        get { return _wealth; }
        set
        {
            float changeMultiplier = 1;
            float change = value - _wealth;
            _wealth += change * changeMultiplier;
            EventsManager.current.StatsChanged(this);
        }
    }

    private float _fitness;
    public float fitness
    {
        get { return _fitness; }
        set
        {
            float changeMultiplier = 1;
            float change = value - _fitness;
            _fitness += change * changeMultiplier;
            _fitness = Mathf.Clamp(value, 0, 100);
            EventsManager.current.StatsChanged(this);
        }
    }

    private float _happiness;
    public float happiness
    {
        get { return _happiness; }
        set
        {
            float change = value - _happiness;
            float changeMultiplier = 1;
            bool changeIsPos = change > 0;
            if (StatusIsActive("fit"))
            {
                if (!changeIsPos)
                {
                    changeMultiplier -= 0.1f;
                }
            }
            if (StatusIsActive("unfit"))
            {
                if (!changeIsPos)
                {
                    changeMultiplier += 0.1f;
                }
            }
            _happiness += change * changeMultiplier;
            _happiness = Mathf.Clamp(value, 0, 100);
            EventsManager.current.StatsChanged(this);
        }
    }

    public Dictionary<string, int> inventory;
    public List<StatusEffect> activeStatuses = new List<StatusEffect>();
    public List<StatusEffect> inactiveStatuses = new List<StatusEffect>();
    public TimelineData timelineData;

    public bool StatusIsActive(string statusName)
    {
        foreach (StatusEffect statusEffect in activeStatuses)
        {
            if (statusEffect.name == statusName)
            {
                return true;
            }
        }

        return false;
    }
}
