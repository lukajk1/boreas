using UnityEngine;

public class VendingMachine : Interactable
{
    [SerializeField] private Light available;
    [SerializeField] private Light outOfStock;
    [SerializeField] private GameObject can;
    private int stock = 3;
    public override void OnInteract()
    {
        if (stock > 0)
        {
            Instantiate(can, transform.position + (new Vector3(0, 0, -1f) * 0.8f), Quaternion.identity);
            stock--;

            if (stock <= 0)
            {
                available.enabled = false;
                outOfStock.enabled = true;
            }
        }

    }
}
