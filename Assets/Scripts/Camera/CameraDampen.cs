using UnityEngine;

public class CameraDampen : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.1f;
    public float delay = 0.02f;

    private Vector3 velocity = Vector3.zero;
    private Vector3 delayedPos;
    private Vector3 dampedPos;

    private float verticalOffset = 0f; // not needed any more since target transform is now tied to a head, but here in case it's useful in the future

    private void LateUpdate()
    {
        Vector3 targetPosition = target.position + new Vector3(0, verticalOffset, 0);

        delayedPos = Vector3.Lerp(delayedPos, targetPosition, Time.deltaTime / delay);

        transform.position = Vector3.SmoothDamp(transform.position, delayedPos, ref velocity, smoothTime);
    }
}
