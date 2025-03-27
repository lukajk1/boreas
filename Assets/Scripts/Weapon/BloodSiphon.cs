using UnityEngine;

public class BloodSiphon : Weapon
{
    public override string Name => "Blood Siphon";
    public override int ClipSize => 99;
    public override int BaseDamage => 5;
    public override float FireRate => 0.03f;
    public override float ReloadSpeed => 1f; // this value is not used
    public override float ReadySpeed => 0.35f;
    public override float LifestealRatio => 0.025f;
    public override float Range => 19f;
    public float RegenDelay => 0.065f;
    public float DelayAfterFiringToStartRegen => 0.5f;

    private BloodSiphonTimer bloodSiphonTimer;

    public override void Fire(Vector3 firingOrigin, Vector3 forwardFacingVector)
    {
        if (base.TryFire())
        {
            bloodSiphonTimer.HasFired();

            if (Physics.Raycast(firingOrigin, forwardFacingVector, out hit, Range))
            {
                ProcessHit(hit);
            }
        }
    }

    public BloodSiphon() 
    {
        bloodSiphonTimer = new GameObject("BloodSiphonTimer").AddComponent<BloodSiphonTimer>();
        bloodSiphonTimer.Setup(this);
    }

    public override void Reload()
    {
        // do nothing
    }
}
