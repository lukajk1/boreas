using UnityEngine;

public class DummySystem : MonoBehaviour
{
    void Start() // place to create temporary setups to plug in intial starting conditions
    {
        MainEventBus.BCOnRunStart();
        FindFirstObjectByType<PlayerUnit>().DebugDamageModifier = 999;
    }
}
