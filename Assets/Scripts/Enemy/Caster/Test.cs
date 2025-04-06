using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            animator.SetTrigger("Cast");
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            Rigidbody[] bodies = GetComponentsInChildren<Rigidbody>();
            foreach (Rigidbody b in bodies)
            {
                b.isKinematic = false;
            }
        }
    }
}
