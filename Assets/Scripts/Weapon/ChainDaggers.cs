using UnityEngine;

public class ChainDaggers : Weapon
{
    public override string Name => "Chain Daggers";
    public override int ClipSize => 7;
    public override int BaseDamage => 45;
    public override float FireRate => 0.63f;
    public override float ReloadSpeed => 1.25f;
    public override float ReadySpeed => 0.35f;
    public override float LifestealRatio => 0.025f;
}
