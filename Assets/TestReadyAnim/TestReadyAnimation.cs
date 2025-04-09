using UnityEngine;

public class TestReadyAnimation : MonoBehaviour
{
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            animator.SetTrigger("Ready");
        }
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Throw");
        }
    }
}
