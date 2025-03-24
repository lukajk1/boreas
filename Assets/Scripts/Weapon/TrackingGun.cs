using UnityEngine;

public class TrackingGun : Weapon
{
    public override string Name => "Tracking Gun";
    public override int ClipSize => 32;
    public override float BaseDamage => 17f;
    public override float FireRate => 0.10f;
    public override float ReloadSpeed => 1.25f;
    public override float ReadySpeed => 0.35f;
    public override float LifestealRatio => 0.025f;
}
