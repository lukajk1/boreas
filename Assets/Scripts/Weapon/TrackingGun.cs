using UnityEngine;

public class TrackingGun : Weapon
{
    public override string Name => "Tracking Gun";
    public override int ClipSize => 32;
    public override float BaseDamage => 17f;
    public override float FireRate => 0.6f;
    public override float LifestealRatio => 0.025f;

    public override bool Fire()
    {
        return true;
    }

    public override void Reload()
    {
        Debug.Log("reloaded");
    }
}
