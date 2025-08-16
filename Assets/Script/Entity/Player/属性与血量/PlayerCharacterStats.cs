using UnityEngine;

public class PlayerCharacterStats : CharacterStats
{
    private Player player;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        player = GetComponent<Player>();
    }

    public override void OnAttacked(int damage)
    {
        base.OnAttacked(damage);
        // 然后播放受击动画
    }
}