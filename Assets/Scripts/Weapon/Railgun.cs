using UnityEngine;

public class Railgun : Weapon
{
    public override string Name => "Railgun";
    public override int ClipSize => 7;
    public override float BaseDamage => 90f;
    public override float FireRate => 1.75f;
    public override float ReloadSpeed => 1.75f;
    public override float LifestealRatio => 0.06f;
}
