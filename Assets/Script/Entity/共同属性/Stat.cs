using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Stat
{
    //人物基础血量和装备血量相加
    [SerializeField] private int baseValue;  //基础属性
    public List<int> modifiers;  //额外属性


    public int GetValue()
    {
        int finalValue = baseValue;
        foreach (int modifier in modifiers)
        {
            finalValue += modifier;
        }
        return finalValue;
    }


    public void AddModifier(int modifier)
    {
        modifiers.Add(modifier);
    }


    public void RemoveModifier(int modifier)
    {
        modifiers.RemoveAt(modifier);
    }
}