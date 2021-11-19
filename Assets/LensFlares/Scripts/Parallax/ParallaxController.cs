using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class ParallaxController : MonoBehaviour
{
    [SerializeField]
    private List<ParallaxLayer> _layers = new List<ParallaxLayer>();
#if UNITY_EDITOR
    [SerializeField]
    private bool _isActiveInEditor = false;
#endif

    private Camera _camera;
    private Transform _cameraTransform;
    private Vector2 _prevCameraPosition;

    private void Awake()
    {
        _camera = GetComponentInChildren<Camera>(true);
        _cameraTransform = _camera.transform;
        LinkLayers();
    }

    private void Start()
    {
        _prevCameraPosition = _cameraTransform.position;
    }

    private void OnValidate()
    {
        LinkLayers();
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (!EditorApplication.isPlaying)
            LateUpdate();
    }
#endif

    private void LateUpdate()
    {
        Vector2 cameraPosition = _cameraTransform.position;
        Vector2 offset = cameraPosition - _prevCameraPosition;
        _prevCameraPosition = cameraPosition;
        Apply(offset);
    }

    private void Apply(Vector2 offset)
    {
#if UNITY_EDITOR
        // suppress work in Editor mode
        if (!EditorApplication.isPlaying && !_isActiveInEditor)
            return;
#endif
        const float kOffsetSqrEpsilon = 1.0E-16f;
        if (offset.sqrMagnitude > kOffsetSqrEpsilon)
        {
            for (int i = 0, count = _layers.Count; i < count; ++i)
            {
                var layer = _layers[i];
                if (layer && layer.isActiveAndEnabled)
                    layer.Apply(offset);
            }
        }
    }

    public void Add(ParallaxLayer layer)
    {
        _layers.Add(layer);
        layer.Link(this);
    }

    public void Remove(ParallaxLayer layer)
    {
        layer.Unlink();
        _layers.Remove(layer);
    }

    private void LinkLayers()
    {
        for (int i = 0, count = _layers.Count; i < count; ++i)
        {
            var layer = _layers[i];
            layer.Link(this);
        }
    }
}
