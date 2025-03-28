using UnityEngine;

public class PlayerUnit : Unit
{
    public override string Name => "Player";
    public override int BaseMaxHealth => 330;
    public override float BaseMoveSpeed => 9.0f;
}
