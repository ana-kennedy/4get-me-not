using UnityEngine;

[RequireComponent(typeof(Camera))]
public class KinoCameraEffect : MonoBehaviour
{
    public Material effectMaterial; // Assign this in the Inspector

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (effectMaterial != null)
        {
            Graphics.Blit(source, destination, effectMaterial);
        }
        else
        {
            Graphics.Blit(source, destination); // No effect if no material assigned
        }
    }
}