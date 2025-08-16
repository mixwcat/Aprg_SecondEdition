using UnityEngine;

public class SecondSceneEvent : MonoBehaviour
{
    private Player player;

    void Start()
    {
        player = FindFirstObjectByType<Player>();

        EventCenter.Trigger("OnPause", null);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnPause(object obj)
    {
        //Time.timeScale = 0; // 暂停游戏
        //player.enabled = false; // 禁用玩家控制
    }

    void OnResume(object obj)
    {
        //Time.timeScale = 1; // 恢复游戏
        //player.enabled = true; // 启用玩家控制
    }

    void OnEnable()
    {
        EventCenter.Subscribe("OnPause", OnPause);
        EventCenter.Subscribe("OnResume", OnResume);
    }

    void OnDisable()
    {
        EventCenter.Unsubscribe("OnPause", OnPause);
        EventCenter.Unsubscribe("OnResume", OnResume);
    }
}
