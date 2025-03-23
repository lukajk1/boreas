using UnityEngine;

public class TrackingGun : Weapon
{
    public override bool Fire()
    {
        return true;
    }

    public override void Reload()
    {
        Debug.Log("reloaded");
    }
}
