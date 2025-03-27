using UnityEngine;

public class LightningGun : Weapon
{
    public override string Name => "Blood Siphon";
    public override int ClipSize => 99;
    public override int BaseDamage => 5;
    public override float FireRate => 0.03f;
    public override float ReloadSpeed => 1.25f;
    public override float ReadySpeed => 0.35f;
    public override float LifestealRatio => 0.025f;
    public override float Range => 15f; 
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
