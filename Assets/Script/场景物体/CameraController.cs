using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region 单例
    public static CameraController Instance { get; private set; }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    #endregion


    [Header("摄像机跟随")]
    public Transform player;
    public Vector3 offset;
    public float smoothSpeed = 0.125f;

    //[Header("摄像机边界")]


    void FixedUpdate()
    {
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        smoothedPosition.z = transform.position.z; // 保持z轴不变
        transform.position = smoothedPosition;
    }


    /// <summary>
    /// 震动停顿
    /// </summary>
    /// <param name="duration"></param>
    public void HitPause(int duration)
    {
        StartCoroutine(Pause(duration));
    }

    private IEnumerator Pause(int duration)
    {
        float pauseTime = duration / 60f;
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(pauseTime);
        Time.timeScale = 1f;
    }


    /// <summary>
    /// 震动摄像机
    /// </summary>
    /// <param name="duration"></param>
    /// <param name="strength"></param>
    public void ShakeCamera(float duration, float strength)
    {
        StartCoroutine(Shake(duration, strength));
    }

    private IEnumerator Shake(float duration, float strength)
    {
        Transform camera = Camera.main.transform;
        Vector3 startPosition = camera.localPosition;
        while (duration > 0)
        {
            camera.localPosition = startPosition + Random.insideUnitSphere * strength;
            duration -= Time.deltaTime;
            yield return null;
        }
        camera.position = startPosition;
    }


    /// <summary>
    /// 订阅事件
    /// </summary>
    void OnEnable()
    {
        //EventCenter.Subscribe("OnEnemyOneHurt", HitPause);
    }

    void OnDisable()
    {
        //EventCenter.Unsubscribe("OnEnemyOneHurt", HitPause);
    }

}
