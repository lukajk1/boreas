using UnityEngine;

public class ZomboUnit : EnemyUnit
{
    public override string Name => "Zombo";
    public override int BaseMaxHealth => 250;
    public override float BaseMoveSpeed => 3.1f;
    public override int ScoreWeight => 100;
}
