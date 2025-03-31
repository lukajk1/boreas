using UnityEngine;

public class Bouncy : EnemyUnit
{
    public override string Name => "Bouncy";
    public override int BaseMaxHealth => 300;
    public override float BaseMoveSpeed => 6f;
    public override int ScoreWeight => 100;
    public override int BaseDamage => 45;
    public override float AttackCDLength => 3.0f;
    public override float AttackRange => 3f;

    protected override void Awake()
    {
        base.Awake();
    }
}
