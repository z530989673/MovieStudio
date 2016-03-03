using UnityEngine;
using System.Collections;

public class SSAO : MonoBehaviour
{

    public Camera m_camera;
    public Material m_SSAOMaterial;
    public int downSampleRate = 1;

    [Range(0.01f, 0.50f)]
    public float radius = 0.125f;

    [Range(0f, 3f)]
    public float intensity = 2.25f;

    [Range(0f, 10f)]
    public float distance = 1f;

    [Range(0f, 1f)]
    public float bias = 0.1f;

    [Range(0f, 1f)]
    public float lumContribution = 0.5f;

    [Range(0f, 10f)]
    public float blurDistance = 1f;

    public Color OcclusionColor = Color.black;

    public float cutoffDistance = 150f;
    public float cutoffFalloff = 50f;

    public Texture2D noiseTexture;

    void Start()
    {
        m_camera.depthTextureMode = DepthTextureMode.DepthNormals; 
        m_camera.depthTextureMode |= DepthTextureMode.Depth;
    }

    void OnRenderImage(RenderTexture source, RenderTexture target)
    {
        RenderTexture ssaoFirstPass = RenderTexture.GetTemporary(source.width / downSampleRate, source.height / downSampleRate, 0);
        m_SSAOMaterial.SetMatrix("_ViewToWorldMatrix", (m_camera.projectionMatrix * m_camera.worldToCameraMatrix).inverse);
        m_SSAOMaterial.SetMatrix("_CameraModelView", m_camera.cameraToWorldMatrix);
        m_SSAOMaterial.SetTexture("_NoiseTexture", noiseTexture);
        m_SSAOMaterial.SetVector("_ParamBiasDis", new Vector4(bias, radius, cutoffDistance, cutoffFalloff));
        m_SSAOMaterial.SetVector("_ParamSSAOAdjust", new Vector4(noiseTexture == null ? 0f : noiseTexture.width, lumContribution, intensity, distance));
        m_SSAOMaterial.SetColor("_OcclusionColor", OcclusionColor);
        Graphics.Blit(null, ssaoFirstPass, m_SSAOMaterial, 0);

        RenderTexture ssaoBlurPass = RenderTexture.GetTemporary(source.width / downSampleRate, source.height / downSampleRate, 0);
        m_SSAOMaterial.SetVector("_Direction", new Vector2(1 / source.width, 0));
        m_SSAOMaterial.SetTexture("_MainTex", ssaoFirstPass);
        Graphics.Blit(null, ssaoBlurPass, m_SSAOMaterial, 1);
        m_SSAOMaterial.SetVector("_Direction", new Vector2(0, 1 / source.height));
        m_SSAOMaterial.SetTexture("_MainTex", ssaoBlurPass);
        Graphics.Blit(null, ssaoFirstPass, m_SSAOMaterial, 1);

        m_SSAOMaterial.SetTexture("_MainTex", source);
        m_SSAOMaterial.SetTexture("_SSAOTex", ssaoFirstPass);
        Graphics.Blit(null, target, m_SSAOMaterial, 2);

        ssaoFirstPass.Release();
        ssaoBlurPass.Release();
    }
}
