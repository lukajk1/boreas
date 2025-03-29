using UnityEngine;

public class ZomboUnit : EnemyUnit
{
    public override string Name => "Zombo";
    public override int BaseMaxHealth => 250;
    public override float BaseMoveSpeed => 5f;
    public override int ScoreWeight => 100;
    public override int BaseDamage => 17;
}
