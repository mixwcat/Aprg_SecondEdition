using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;


public class Skills_SO : ScriptableObject
{
    public string skillName;
    public float cooldownTime;
    protected float lastTimeUsed;


    public virtual void UseSkill()
    {
        if (CanUseSkill())
        {
            // 实现技能的具体逻辑
            Debug.Log($"{skillName}技能被使用");
        }
        else
        {
            Debug.Log($"{skillName}技能冷却中");
        }
    }


    protected virtual bool CanUseSkill()
    {
        if (Time.time - lastTimeUsed >= cooldownTime || lastTimeUsed == 0)
        {
            lastTimeUsed = Time.time;
            return true;
        }
        return false;
    }
}
