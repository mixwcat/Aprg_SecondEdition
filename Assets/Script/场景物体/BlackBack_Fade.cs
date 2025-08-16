using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BlackBack_Fade : MonoBehaviour
{
    public static BlackBack_Fade Instance;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public Image blackImage;

    public void FadeOut()
    {
        blackImage.DOFade(0, 1).From(1);
    }

    public void FadeIn()
    {
        blackImage.DOFade(1, 1).From(0);
    }

}
