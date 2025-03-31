using UnityEngine;

public class Diver : EnemyUnit
{
    public override string Name => "Diver";
    public override int BaseMaxHealth => 650;
    public override float BaseMoveSpeed => 5f;
    public override int ScoreWeight => 100;
    public override int BaseDamage => 45;
    public override float AttackCDLength => 4f;
    public override float AttackRange => 17f;

    protected override void Awake()
    {
        base.Awake();
    }
}
