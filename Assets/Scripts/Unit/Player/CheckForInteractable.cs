using UnityEngine;

public class CheckForInteractable : MonoBehaviour
{
    [SerializeField] private Camera fpCamera;

    private const float Range = 5.0f;

    private RaycastHit hit;
    [SerializeField] private InteractPrompt interactPrompt;

    bool currentInteractState;
    bool isInteractable;

    bool pressedInteract;
    private void Start()
    {
        
    }
    private void Update()
    {
        isInteractable = false;

        pressedInteract = Input.GetKeyDown(KeyCode.E);

        if (Physics.Raycast(fpCamera.transform.position, fpCamera.transform.forward, out hit, Range))
        {
            isInteractable = hit.collider.CompareTag("Interact");
        }

        if (isInteractable != currentInteractState)
        {
            currentInteractState = isInteractable;
            interactPrompt.SetInteract(currentInteractState);
        }

        if (isInteractable && pressedInteract)
        {
            hit.collider.GetComponent<Interactable>().OnInteract();
        }
    }
}
