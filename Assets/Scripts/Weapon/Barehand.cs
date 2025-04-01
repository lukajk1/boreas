using UnityEngine;

public class Barehand : Weapon
{
    public override string Name => "Barehand";
    public override int ClipSize => 41;
    public override int BaseDamage => 22;
    public override float FireRate => 0.10f;
    public override float ReloadSpeed => 1.25f;
    public override float ReadySpeed => 0.35f;
    public override float LifestealRatio => 0.025f;
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
