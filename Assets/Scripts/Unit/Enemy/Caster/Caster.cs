using UnityEngine;

public class Caster : EnemyUnit
{
    public override string Name => "Caster";
    public override int BaseMaxHealth => 250;
    public override float BaseMoveSpeed => 5f;
    public override int ScoreWeight => 115;
    public override int BaseDamage => 24;
}
