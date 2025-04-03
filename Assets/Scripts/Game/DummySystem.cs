using UnityEngine;

public class DummySystem : MonoBehaviour
{
    //public bool Invulnerable;
    void Start() // place to create temporary setups to plug in intial starting conditions
    {
        MainEventBus.BCOnRunStart();
        FindFirstObjectByType<PlayerUnit>().DebugDamageModifier = 0;
    }
}
