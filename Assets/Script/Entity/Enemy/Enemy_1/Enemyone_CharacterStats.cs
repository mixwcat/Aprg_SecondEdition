using UnityEngine;

public class Enemyone_CharacterStats : CharacterStats
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    void OnEnable()
    {
        EventCenter.Subscribe("OnEnemyOneDeath", OnEnemyOneDeath);
    }
    void OnDisable()
    {
        EventCenter.Unsubscribe("OnEnemyOneDeath", OnEnemyOneDeath);
    }

    private void OnEnemyOneDeath(object obj)
    {

    }

    protected override void Die()
    {
        EventCenter.Trigger("OnEnemyOneDeath", null);
        base.Die();
    }
}
