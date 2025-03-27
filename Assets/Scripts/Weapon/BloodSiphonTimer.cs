using System;
using System.Collections;
using UnityEngine;

public class BloodSiphonTimer : MonoBehaviour
{
    private BloodSiphon weapon;
    private Coroutine regenDelay;
    private Coroutine hasFired;
    private float regenDelayLength;
    public void Setup(BloodSiphon weapon)
    {
        this.weapon = weapon;
        regenDelayLength = weapon.RegenDelay;
    }

    private void Update()
    {
        if (regenDelay == null && hasFired == null)
        {
            regenDelay = StartCoroutine(Regen(regenDelayLength, () => regenDelay = null));
        }
    }

    private IEnumerator Regen(float duration, Action onComplete)
    {
        yield return new WaitForSeconds(duration);
        weapon.ModifyCurrentAmmo(1);
        onComplete?.Invoke();
    }

    private IEnumerator Timer(float duration, Action onComplete)
    {
        yield return new WaitForSeconds(duration);
        onComplete?.Invoke();
    }

    public void HasFired()
    {
        if (hasFired != null)
        {
            StopCoroutine(hasFired);
        }
        hasFired = StartCoroutine(Timer(weapon.DelayAfterFiringToStartRegen, () => hasFired = null));
    }
}
