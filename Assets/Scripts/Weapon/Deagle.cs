using UnityEngine;

public class Deagle : Weapon
{
    public override string Name => "Deagle";
    public override int ClipSize => 7;
    public override float BaseDamage => 45f;
    public override float FireRate => 0.63f;
    public override float ReloadSpeed => 1.25f;
    public override float ReadySpeed => 0.35f;
    public override float LifestealRatio => 0.025f;
}
