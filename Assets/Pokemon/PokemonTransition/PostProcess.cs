using UnityEngine;

[ExecuteInEditMode]
public class PostProcess : MonoBehaviour
{
    public Material material;
    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (material == null)
        {
            return;
        }
        Graphics.Blit(src, dest, material);   
    }
}
