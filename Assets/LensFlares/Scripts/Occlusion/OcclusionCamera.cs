using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering;

[ExecuteInEditMode]
public class OcclusionCamera : MonoBehaviour
{
    private const string kReplacementTag = "RenderType";

    [Serializable]
    public class OccludePresest
    {
        public float size;
        public float clipNear;
        public float clipFar;
        public Vector3 offset;
    }

    [SerializeField]
    public Camera _camera;
    [SerializeField]
    private Shader _shader;
    [SerializeField]
    private Renderer _target;
    [SerializeField]
    private OcclusionPreset _preset;
    public LensFlares lensFlare;
    public int id;

    private void OnDestroy()
    {
        UIManager.instance.flares.Remove(lensFlare);

        if (lensFlare != null)
            Destroy(lensFlare.gameObject);
    }

    public void AwakeW()
    {
#if UNITY_EDITOR
        if (_camera != null)
#endif
        {
            _camera.enabled = false;
            RenderTexture renderTexture = new RenderTexture(_camera.targetTexture);
            _camera.targetTexture = renderTexture;
            lensFlare.renderTexture = new Texture2D(64, 64, TextureFormat.R16, false, true);
            float size = _preset.presets[id].size;
            transform.localScale = new Vector3(size, size, size);
            _camera.nearClipPlane = _preset.presets[id].clipNear;
            _camera.farClipPlane = _preset.presets[id].clipFar;
            lensFlare.camera = this;
        }
    }

    public void UpdateW()
    {
#if UNITY_EDITOR
        if (_camera != null &&
            _shader != null &&
            _target != null)
#endif
        {
            var bounds = _target.bounds;
            var size = bounds.size;
            var maxSize = Mathf.Max(size.x, size.y);
            var orthoSize = maxSize * 0.5f;
            _camera.orthographicSize = orthoSize;
            Vector3 screenPoint = LevelManager.instance.defaultCamera.WorldToScreenPoint(transform.position);
            lensFlare.transform.position = screenPoint;
            lensFlare.distance = Vector3.Distance(transform.position, LevelManager.instance.defaultCamera.transform.position);
            lensFlare.UpdateW();
        }
    }

    public void RenderW()
    {
        var originalPos = transform.localPosition;
        transform.localPosition += _preset.presets[id].offset;
        transform.LookAt(LevelManager.instance.defaultCamera.transform);
        _camera.RenderWithShader(_shader, kReplacementTag);
        transform.localPosition = originalPos;

        if (SystemInfo.supportsAsyncGPUReadback)
        {
            if (lensFlare.request.done)
            {
                var request = AsyncGPUReadback.Request(_camera.activeTexture);
                lensFlare.request = request;
            }
        }
    }
}
