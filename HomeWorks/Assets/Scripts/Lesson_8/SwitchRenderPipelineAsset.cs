using UnityEngine;
using UnityEngine.Rendering;

public class SwitchRenderPipelineAsset : MonoBehaviour
{
    public RenderPipelineAsset exampleAssetA;
    public RenderPipelineAsset exampleAssetB;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            GraphicsSettings.renderPipelineAsset = exampleAssetA;
            Debug.Log("Default render pipeline asset is: " + GraphicsSettings.renderPipelineAsset.name);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            GraphicsSettings.renderPipelineAsset = exampleAssetB;
            Debug.Log("Default render pipeline asset is: " + GraphicsSettings.renderPipelineAsset.name);
        }
    }

     
}