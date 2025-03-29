using UnityEngine;

public class FlyingAgentAvoidance : MonoBehaviour
{
    public float speed = 5f;
    public float obstacleAvoidanceRadius = 5f;  // Distance to check for obstacles
    public float avoidanceStrength = 10f;  // How strongly to avoid obstacles
    public Transform target;

    private void Update()
    {
        // Check for obstacles in front of the agent using raycasting
        Vector3 forward = transform.forward;
        Vector3 position = transform.position;

        RaycastHit hit;

        // Raycast ahead of the agent to check for obstacles
        if (Physics.Raycast(position, forward, out hit, obstacleAvoidanceRadius))
        {
            // If obstacle is detected, steer in the opposite direction
            Vector3 avoidanceDirection = Vector3.Reflect(forward, hit.normal);  // Calculate reflection direction
            transform.position += avoidanceDirection * avoidanceStrength * Time.deltaTime;
        }
        else
        {
            // If no obstacles, move towards the target
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            transform.position += directionToTarget * speed * Time.deltaTime;
        }

        // Optional: Smoothly adjust rotation to face the direction of movement
        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
    }
}
