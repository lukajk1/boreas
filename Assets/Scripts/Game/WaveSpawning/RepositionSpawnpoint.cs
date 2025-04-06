using UnityEngine;

public class RepositionSpawnpoint : MonoBehaviour
{
    private float distance;
    private void Start()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            distance = hit.distance;
        }

    }
}
