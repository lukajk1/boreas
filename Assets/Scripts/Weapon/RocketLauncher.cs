using UnityEngine;

public class RocketLauncher : Weapon
{
    public override string Name => "Rocket Launcher";
    public override int ClipSize => 7;
    public override int BaseDamage => 65;
    public override float FireRate => 0.70f;
    public override float ReloadSpeed => 1.25f;
    public override float ReadySpeed => 0.65f;
    public override float LifestealRatio => 0.07f;
}
