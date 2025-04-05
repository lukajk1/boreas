using UnityEngine;

public class SpinnerEyeLookAtPlayer : MonoBehaviour
{

    void Update()
    {
        transform.LookAt(Game.i.PlayerTransform.position);
    }
}
