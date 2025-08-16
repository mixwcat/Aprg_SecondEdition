using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    [Header("血量")]
    public int currentHp;
    public Stat maxHp;  //最大血量
    public Image HealthIg;


    [Header("血量渐变")]
    public float HpFade;    //减少血量时，血量渐变
    public Image HealthFadeIg;
    public float FadeTimer;
    public int FadeTime = 1;


    [Header("伤害")]
    public Stat damage;


    protected virtual void Start()
    {
        currentHp = maxHp.GetValue();
    }


    protected virtual void Update()
    {
        //Hp_Fade();
    }


    /// <summary>
    /// Lerp逐渐减少HpFade，HealthFadeIg逐渐变短
    /// </summary>
    private void Hp_Fade()
    {
        FadeTimer += Time.deltaTime;
        if (currentHp < HpFade && FadeTimer >= FadeTime)
        {
            HpFade = Mathf.Lerp(HpFade, currentHp, Time.deltaTime * 2);
        }
        HealthIg.fillAmount = currentHp / 100f;
        HealthFadeIg.fillAmount = HpFade / 100f;
    }


    public virtual void DoDamage(CharacterStats _target)
    {
        if (_target == null)
        {
            return;
        }
        if (_target != null)
        {
            _target.OnAttacked(damage.GetValue());
        }
    }


    //计算血量减少
    public virtual void OnAttacked(int damage)
    {
        currentHp -= damage;
        if (currentHp <= 0)
        {
            Die();
        }
    }


    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}