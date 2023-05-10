using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField] private float baseValue;
    private List<int> modifiers = new List<int>();

    public void SetValue(float value)
    {
        if (value <= 0)
            return;

        baseValue = value;
    }

    public float GetValue()
    {
        float finalValue = baseValue;
        modifiers.ForEach(mod => finalValue += mod);
        return finalValue;
    }

    public void AddModifier(int modifier)
    {
        if (modifier != 0)
        {
            modifiers.Add(modifier);
        }
    }

    public void RemoveModifier(int modifier)
    {
        if (modifier != 0)
        {
            modifiers.Remove(modifier);
        }
    }
}
