using UnityEngine;

public class FOV : MonoBehaviour
{
    private Camera cam;
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    public void SetFOV(float fov)
    {
        cam.fieldOfView = fov;
    }
}
