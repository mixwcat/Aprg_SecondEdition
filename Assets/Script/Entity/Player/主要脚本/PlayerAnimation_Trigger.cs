using UnityEngine;

public class PlayerAnimation_Trigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnAttackTrigger()
    {
        // 触发攻击动画结束事件
        EventCenter.Trigger("OnPlayerAttackOver");
    }


}
