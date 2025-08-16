using NUnit.Framework.Internal;
using UnityEngine;

[CreateAssetMenu(fileName = "TestSkill", menuName = "Skills/TestSkill")]
public class TestSkill_SO : Skills_SO
{
    private void OnEnable()
    {
        skillName = "Test Skill";
        cooldownTime = 1.5f;
        lastTimeUsed = 0;
    }

    public GameObject TestSkillPrefab;


    public override void UseSkill()
    {
        if (CanUseSkill())
        {
            Instantiate(TestSkillPrefab, PlayerManager.Instance.player.transform.position, Quaternion.identity);
        }
        else
        {
            Debug.Log(skillName + "冷却时间:" + (cooldownTime - (Time.time - lastTimeUsed)) + "秒");
        }
    }
}
