using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

// TODO: Remove for URP 13.
// https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@13.1/manual/upgrade-guide-2022-1.html
#pragma warning disable CS0618

namespace FlatKit {
public class FlatKitFog : ScriptableRendererFeature {
    class EffectPass : ScriptableRenderPass {
        private readonly ProfilingSampler _profilingSampler = new ProfilingSampler("Flat Kit Fog");
        private RTHandle _destination;
        private readonly Material _effectMaterial;
        private RenderTargetHandle _temporaryColorTexture;

        public EffectPass(Material effectMaterial) {
            _effectMaterial = effectMaterial;
        }

        public void Setup(RTHandle destination = null) {
            _destination = destination;
#if UNITY_2020_3_OR_NEWER
            ConfigureInput(ScriptableRenderPassInput.Depth);
#endif
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData) {
            CommandBuffer cmd = CommandBufferPool.Get();
            using (new ProfilingScope(cmd, _profilingSampler)) {
                RenderTextureDescriptor opaqueDescriptor = renderingData.cameraData.cameraTargetDescriptor;
                opaqueDescriptor.depthBufferBits = 0;

#if UNITY_2022_1_OR_NEWER
                var cameraTargetHandle = renderingData.cameraData.renderer.cameraColorTargetHandle;
#else
                var cameraTargetHandle = renderingData.cameraData.renderer.cameraColorTarget;
#endif

                if (_destination == null) {
                    cmd.GetTemporaryRT(_temporaryColorTexture.id, opaqueDescriptor, FilterMode.Point);
                    cmd.Blit(cameraTargetHandle, _temporaryColorTexture.Identifier(), _effectMaterial);
                    cmd.Blit(_temporaryColorTexture.Identifier(), cameraTargetHandle);
                } else {
                    cmd.Blit(cameraTargetHandle, _destination, _effectMaterial);
                }
            }

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }

#if UNITY_2020_3_OR_NEWER
        public override void OnCameraCleanup(CommandBuffer cmd) {
#else
        public override void FrameCleanup(CommandBuffer cmd) {
#endif
            cmd.ReleaseTemporaryRT(_temporaryColorTexture.id);
        }
    }

    [Header("Create > FlatKit > Fog Settings")]
    public FogSettings settings;

    private Material _material;
    private EffectPass _effectPass;

    private Texture2D _lutDepth;
    private Texture2D _lutHeight;

    private static readonly string ShaderName = "Hidden/FlatKit/FogFilter";
    private static readonly int DistanceLut = Shader.PropertyToID("_DistanceLUT");
    private static readonly int Near = Shader.PropertyToID("_Near");
    private static readonly int Far = Shader.PropertyToID("_Far");
    private static readonly int UseDistanceFog = Shader.PropertyToID("_UseDistanceFog");
    private static readonly int UseDistanceFogOnSky = Shader.PropertyToID("_UseDistanceFogOnSky");
    private static readonly int DistanceFogIntensity = Shader.PropertyToID("_DistanceFogIntensity");
    private static readonly int HeightLut = Shader.PropertyToID("_HeightLUT");
    private static readonly int LowWorldY = Shader.PropertyToID("_LowWorldY");
    private static readonly int HighWorldY = Shader.PropertyToID("_HighWorldY");
    private static readonly int UseHeightFog = Shader.PropertyToID("_UseHeightFog");
    private static readonly int UseHeightFogOnSky = Shader.PropertyToID("_UseHeightFogOnSky");
    private static readonly int HeightFogIntensity = Shader.PropertyToID("_HeightFogIntensity");
    private static readonly int DistanceHeightBlend = Shader.PropertyToID("_DistanceHeightBlend");

    public override void Create() {
        if (settings == null) {
            Debug.LogWarning("[FlatKit] Missing Fog Settings");
            return;
        }

        InitMaterial();

        _effectPass = new EffectPass(_material) {
            renderPassEvent = settings.renderEvent
        };
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData) {
#if UNITY_EDITOR
        if (renderingData.cameraData.isPreviewCamera) {
            return;
        }
#endif

        if (settings == null) {
            Debug.LogWarning("[FlatKit] Missing Fog Settings");
            return;
        }

        InitMaterial();

        _effectPass.Setup(null);
        renderer.EnqueuePass(_effectPass);
    }

#if UNITY_2020_3_OR_NEWER
    protected override void Dispose(bool disposing) {
        CoreUtils.Destroy(_material);
    }
#endif

    private void InitMaterial() {
#if UNITY_EDITOR
        ShaderIncludeUtilities.AddAlwaysIncludedShader(ShaderName);
#endif

        if (_material == null) {
            var shader = Shader.Find(ShaderName);
            if (shader == null) {
                return;
            }

            _material = new Material(shader);
        }

        if (_material == null) {
            Debug.LogWarning("[FlatKit] Missing Fog Material");
        }

        UpdateShader();
    }

    private void UpdateShader() {
        if (_material == null) {
            return;
        }

        UpdateDistanceLut();
        _material.SetTexture(DistanceLut, _lutDepth);
        _material.SetFloat(Near, settings.near);
        _material.SetFloat(Far, settings.far);
        _material.SetFloat(UseDistanceFog, settings.useDistance ? 1f : 0f);
        _material.SetFloat(UseDistanceFogOnSky, settings.useDistanceFogOnSky ? 1f : 0f);
        _material.SetFloat(DistanceFogIntensity, settings.distanceFogIntensity);

        UpdateHeightLut();
        _material.SetTexture(HeightLut, _lutHeight);
        _material.SetFloat(LowWorldY, settings.low);
        _material.SetFloat(HighWorldY, settings.high);
        _material.SetFloat(UseHeightFog, settings.useHeight ? 1f : 0f);
        _material.SetFloat(UseHeightFogOnSky, settings.useHeightFogOnSky ? 1f : 0f);
        _material.SetFloat(HeightFogIntensity, settings.heightFogIntensity);

        _material.SetFloat(DistanceHeightBlend, settings.distanceHeightBlend);
    }

    private void UpdateDistanceLut() {
        if (settings.distanceGradient == null) return;

        if (_lutDepth != null) {
            DestroyImmediate(_lutDepth);
        }

        const int width = 256;
        const int height = 1;
        _lutDepth = new Texture2D(width, height, TextureFormat.RGBA32, /*mipChain=*/false) {
            wrapMode = TextureWrapMode.Clamp,
            hideFlags = HideFlags.HideAndDontSave,
            filterMode = FilterMode.Bilinear
        };

        //22b5f7ed-989d-49d1-90d9-c62d76c3081a

        for (float x = 0; x < width; x++) {
            Color color = settings.distanceGradient.Evaluate(x / (width - 1));
            for (float y = 0; y < height; y++) {
                _lutDepth.SetPixel(Mathf.CeilToInt(x), Mathf.CeilToInt(y), color);
            }
        }

        _lutDepth.Apply();
    }

    private void UpdateHeightLut() {
        if (settings.heightGradient == null) return;

        if (_lutHeight != null) {
            DestroyImmediate(_lutHeight);
        }

        const int width = 256;
        const int height = 1;
        _lutHeight = new Texture2D(width, height, TextureFormat.RGBA32, /*mipChain=*/false) {
            wrapMode = TextureWrapMode.Clamp,
            hideFlags = HideFlags.HideAndDontSave,
            filterMode = FilterMode.Bilinear
        };

        for (float x = 0; x < width; x++) {
            Color color = settings.heightGradient.Evaluate(x / (width - 1));
            for (float y = 0; y < height; y++) {
                _lutHeight.SetPixel(Mathf.CeilToInt(x), Mathf.CeilToInt(y), color);
            }
        }

        _lutHeight.Apply();
    }
}
}