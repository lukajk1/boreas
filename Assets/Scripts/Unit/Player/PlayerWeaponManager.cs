using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponManager : MonoBehaviour
{
    [SerializeField] private Camera fpCamera;
    [SerializeField] private PlayerInput playerInput;
    private Weapon weapon;
    private InputAction attackAction;
    private void OnEnable()
    {
        CombatEventBus.OnActiveWeaponChanged += UpdateActiveWeapon;
        playerInput.actions["Reload"].performed += OnReloadPressed;
        attackAction = playerInput.actions["Attack"];
        attackAction.Enable();
    }
    private void OnDisable()
    {
        CombatEventBus.OnActiveWeaponChanged -= UpdateActiveWeapon;
        playerInput.actions["Reload"].performed -= OnReloadPressed;
        attackAction.Disable();
    }
    public void Setup()
    {
        UpdateActiveWeapon();
    }
    private void Update()
    {
        if (attackAction.IsPressed())
        {
            weapon.Fire(fpCamera.transform.position, fpCamera.transform.forward);
        }
    }

    private void UpdateActiveWeapon()
    {
        weapon = Inventory.I.GetActiveWeapon();
    }

    private void OnReloadPressed(InputAction.CallbackContext ctx)
    {
        weapon.Reload();
    }

}
