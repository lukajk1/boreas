using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    private void Awake()
    {
        gameObject.tag = "Interact";
    }
    public abstract void OnInteract();
}
