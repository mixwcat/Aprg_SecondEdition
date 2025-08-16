using UnityEngine;

public class SkillManager : MonoBehaviour
{
    #region 单例模式
    public static SkillManager Instance;
    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance.gameObject);
        else
            Instance = this;
    }
    #endregion


    public Skills_SO[] skills_List;

    public void Try_UseSkill<T>() where T : Skills_SO
    {
        foreach (var skill in skills_List)
        {
            if (skill is T) // 检查技能类型
            {
                skill.UseSkill(); // 调用技能的使用方法
            }
        }
        return;
    }
}
