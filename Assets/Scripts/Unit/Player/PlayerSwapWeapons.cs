using UnityEngine;
using UnityEngine.InputSystem;



public class PlayerSwapWeapons : MonoBehaviour
{

    private Inventory inventory;

    private void Start()
    {
        inventory = Inventory.Instance;
    }
    private void Update()
    {
        Vector2 scroll = Mouse.current.scroll.ReadValue();
        float scrollY = scroll.y;

        if (scrollY > 0)
        {
            inventory.SwapActiveWeapon();
            Debug.Log("scroll up");
        }
        else if (scrollY < 0)
        {
            inventory.SwapActiveWeapon();
            Debug.Log("scroll upd");
        }


        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            inventory.SetActiveSlot(0);
        }
        else if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            inventory.SetActiveSlot(1);
        }
    }
}
