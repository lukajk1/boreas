using UnityEngine;

[ExecuteInEditMode]
public class ApplyImageEffects : MonoBehaviour
{
    [SerializeField] private Material pixellateMaterial;
    [SerializeField] private RenderTexture renderTexture;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        //renderTexture = new RenderTexture(800, 450, 0);
        //Graphics.Blit(source, renderTexture, pixellateMaterial);

        Graphics.Blit(source, destination, pixellateMaterial);
    }

}
