using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponManager : MonoBehaviour
{
    [SerializeField] private Camera fpCamera;
    [SerializeField] private PlayerInput playerInput;
    private Weapon weapon;

    private void OnEnable()
    {
        CombatEventBus.OnActiveWeaponChanged += UpdateActiveWeapon;
        playerInput.actions["Reload"].performed += OnReloadPressed;
    }
    private void OnDisable()
    {
        CombatEventBus.OnActiveWeaponChanged -= UpdateActiveWeapon;
        playerInput.actions["Reload"].performed -= OnReloadPressed;
    }
    public void Setup()
    {
        UpdateActiveWeapon();
    }

    private void UpdateActiveWeapon()
    {
        weapon = Inventory.I.GetActiveWeapon();
    }

    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            weapon.Fire(fpCamera.transform.position, fpCamera.transform.forward);
        }
    }
    private void OnReloadPressed(InputAction.CallbackContext ctx)
    {
        weapon.Reload();
    }
}
