using UnityEngine;

public class Railgun : Weapon
{
    public override string Name => "Railgun";
    public override int ClipSize => 7;
    public override int BaseDamage => 75;
    public override float FireRate => 1.75f;
    public override float ReloadSpeed => 1.75f;
    public override float ReadySpeed => 0.7f;
    public override float LifestealRatio => 0.06f;
    public override float Range => 999f;

    public override void Fire(Vector3 firingOrigin, Vector3 forwardFacingVector)
    {
        if (base.TryFire())
        {
            if (Physics.Raycast(firingOrigin, forwardFacingVector, out hit, Range))
            {
                ProcessHit(hit);
            }
        }
    }
}
