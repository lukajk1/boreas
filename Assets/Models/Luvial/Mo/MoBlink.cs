using System.Collections;
using UnityEngine;

public class MoBlink : MonoBehaviour
{
    [SerializeField] private Material normal;
    [SerializeField] private Material blink;
    [SerializeField] private MeshRenderer mesh;
    private float blinkLength = 0.2f;

    private void Start()
    {
        StartCoroutine(Blink());
    }

    private IEnumerator Blink()
    {
        while (true)
        {
            mesh.material = blink;
            yield return new WaitForSeconds(blinkLength);

            mesh.material = normal;
            float randomInterval = Random.Range(4f, 8f);
            yield return new WaitForSeconds(randomInterval);
        }
    }
}
