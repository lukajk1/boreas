using UnityEngine;

public class DummySystem : MonoBehaviour
{
    public bool Invulnerable;
    public bool DieInOneHit;
    void Start() // place to create temporary setups to plug in intial starting conditions
    {
        MainEventBus.BCOnRunStart();
        if (Invulnerable) FindAnyObjectByType<PlayerUnit>().DebugDamageModifier = 0;
        else if (DieInOneHit) FindAnyObjectByType<PlayerUnit>().DebugDamageModifier = 999;
    }
}
