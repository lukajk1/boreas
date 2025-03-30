using UnityEngine;

public abstract class UnitController : MonoBehaviour
{
    protected Timer timer;
    protected virtual void Start()
    {
        timer = gameObject.AddComponent<Timer>();
    }
}
