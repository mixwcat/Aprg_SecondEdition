using UnityEngine;
using UnityEngine.UI;

public class Enemyone_CharacterStats : CharacterStats
{
    public Slider hp_Slider;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        hp_Slider.maxValue = maxHp.GetValue();
        hp_Slider.value = currentHp;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    void OnEnable()
    {
        EventCenter.Subscribe("OnEnemyOneDeath", OnEnemyOneDeath);
        EventCenter.Subscribe("OnEnemyOneHurt", OnEnemyOneHurt);
    }
    void OnDisable()
    {
        EventCenter.Unsubscribe("OnEnemyOneDeath", OnEnemyOneDeath);
        EventCenter.Unsubscribe("OnEnemyOneHurt", OnEnemyOneHurt);
    }

    private void OnEnemyOneDeath(object obj)
    {

    }

    private void OnEnemyOneHurt(object obj)
    {
        hp_Slider.value = currentHp;
    }

    protected override void Die()
    {
        EventCenter.Trigger("OnEnemyOneDeath", null);
        base.Die();
    }
}
