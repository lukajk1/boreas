using UnityEngine;

public class SpawnDroppedWeapon : MonoBehaviour
{
    [SerializeField] private GameObject daggerDrop;
    [SerializeField] private GameObject sniperDrop;
    [SerializeField] private GameObject siphonDrop;
    [SerializeField] private GameObject barehandDrop;
    private void OnEnable()
    {
        CombatEventBus.OnWeaponDropped += CreateDrop;
    }
    private void OnDisable()
    {
        CombatEventBus.OnWeaponDropped -= CreateDrop;
    }


    public void CreateDrop(Weapon droppedWeapon)
    {
        GameObject drop;

        switch (droppedWeapon)
        {
            case Barehand:
                drop = barehandDrop;
                    break;
            case BloodSiphon:
                drop = siphonDrop;
                    break;
            case ChainDaggers:
                drop = daggerDrop;
                    break;
            case Spear:
                drop = sniperDrop;
                    break;
            default:
                drop = barehandDrop;
                break;
        }

        GameObject weaponDrop = Instantiate(drop, Game.I.PlayerTransform.position + new Vector3(0, 1.5f, 0) + Game.I.PlayerCamera.transform.forward * 1.5f, Quaternion.identity);
        weaponDrop.GetComponentInChildren<WeaponPickup>().SetWeapon(droppedWeapon);

        //Debug.Log($"instantiated at {Game.I.PlayerTransform.position + new Vector3(0, 2f, 0)}");
        //Debug.Log($"player pos: {Game.I.PlayerTransform.position}");
    }
}
