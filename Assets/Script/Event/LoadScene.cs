using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadScene : MonoBehaviour
{
    # region 单例模式
    public static LoadScene Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    #endregion


    /// <summary>
    /// 进入指定场景
    /// </summary>
    /// <param name="sceneName"></param>
    public void EnterScene(string sceneName)
    {
        BlackBack_Fade.Instance.FadeIn(); // 淡入黑色背景
        StartCoroutine(LoadSceneCoroutine(sceneName));
    }

    /// <summary>
    /// 协程实现异步加载
    /// </summary>
    /// <param name="sceneName"></param>
    /// <returns></returns>
    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        // 检查场景是否有效
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("Scene name is null or empty. Cannot load scene.");
            yield break; // 终止协程
        }

        // 检查场景是否存在
        if (!Application.CanStreamedLevelBeLoaded(sceneName))
        {
            Debug.LogError($"Scene '{sceneName}' cannot be found or is not added to the build settings.");
            yield break; // 终止协程
        }

        // 开始异步加载场景（Additive 模式）
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = false; // 禁止自动激活场景

        // 等待加载完成
        while (asyncOperation.progress < .9f)
        {
            yield return null; // 等待加载进度达到 1
        }

        // 等待 1 秒，期间禁止移动
        EventCenter.Trigger("OnPause", null);
        yield return new WaitForSeconds(1f);

        // 激活场景
        asyncOperation.allowSceneActivation = true;

        // 等待场景激活完成
        while (!asyncOperation.isDone)
        {
            yield return null;
        }

        // 设置新场景为激活场景
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));

        // 淡出黑色背景
        BlackBack_Fade.Instance.FadeOut();
    }


    #region 切换场景清除订阅
    private void OnEnable()
    {
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    private void OnSceneUnloaded(Scene scene)
    {
        EventCenter.ClearAll(); // 在场景卸载时清除所有订阅
    }
    #endregion
}