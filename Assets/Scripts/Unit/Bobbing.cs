using UnityEngine;

public class Bobbing : MonoBehaviour
{
    [SerializeField] private float bobbingSpeed = 2f;
    [SerializeField] private float bobbingHeight = 0.2f;

    private float originalY;
    private bool canBob;
    private void Update()
    {
        if (canBob) ApplyBobbing();
    }
    public void StartBobbing()
    {
        originalY = transform.position.y;
        canBob = true;
    }
    public void Stop()
    {
        canBob = false;
    }
    void ApplyBobbing()
    {
        float newY = originalY + Mathf.Sin(Time.time * bobbingSpeed) * bobbingHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
