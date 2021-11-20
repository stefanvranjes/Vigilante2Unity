using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
#if UNITY_EDITOR
using UnityEditor;
#endif

using Random = UnityEngine.Random;

public class LensFlares : MonoBehaviour
{
    private class Flare
    {
        private const string kMeshName = "Flare Quad";
        private const string kObjectName = "Flare #{0}";

        private static readonly int kMainTexPropId = Shader.PropertyToID("_MainTex");
        private static readonly Vector3 kForward = Vector3.forward;

        private RawImage _rawImage;
        private GameObject _self;
        private RectTransform _transform;
        private RectTransform _ownerTransform;
        private LensFlares _owner;
        private FlarePreset _preset;

        private Vector3 localScale
        {
            get
            {
                float size = _preset.size;
                return new Vector3(size, size, 1.0f);
            }
        }

        public Flare(LensFlares owner, int index)
        {
            var flares = owner._presets.presets;
            _preset = flares[index];
            _owner = owner;
            _ownerTransform = (RectTransform)owner.transform;
            CreateObject(index);
            ApplyParams();
            Update();
        }

        public void Refresh()
        {
            ApplyParams();
        }

        public void Clear()
        {
            if (_self != null)
            {
                Destroy(_self);
            }
        }

        public void Update()
        {
            Vector2 lightPosition = _ownerTransform.anchoredPosition;
            //Vector2 cameraPosition = _owner._cameraTransform.position;
            //cameraPosition += _owner._cameraOffset;
            Vector2 position = Vector2.LerpUnclamped(lightPosition, Vector2.zero, _preset.position);
            //_transform.LookAt(Vector3.zero);
            //_transform.localEulerAngles = new Vector3(0, 0, _transform.localEulerAngles.x);

            if (_preset.align)
            {
                var direction = position - lightPosition;
                direction = direction.Rotate(_preset.startAngle);
                var rotation = Quaternion.LookRotation(kForward, direction);
                _transform.SetPositionAndRotation(position, rotation);
                _transform.anchoredPosition = position;
            }
            //else
            /*{
                _transform.anchoredPosition = position;
            }*/

            float distance = Mathf.Clamp(_owner.distance / _owner.divider, _owner.min, _owner.max);
            _transform.localScale = new Vector3(distance, distance);
        }

        private void CreateObject(int index)
        {
            string name = string.Format(kObjectName, index);
            _self = new GameObject(name, typeof(RawImage));
            _transform = (RectTransform)_self.transform;
            _transform.SetParent(_ownerTransform);
            _rawImage = _self.GetComponent<RawImage>();
            
        }

        private void ApplyParams()
        {
            _transform.localScale = localScale;
            _rawImage.material = _owner._material;
            _rawImage.texture = _preset.sprite;
            _transform.sizeDelta = new Vector2(_preset.sprite.width, _preset.sprite.height);
        }
    }

    [Serializable]
    public class FlarePreset
    {
        public Texture2D sprite;
        public Color color = Color.white;
        public float size = 1.0f;
        public float position = 0.0f;
        public bool align = true;
        public float startAngle = 0.0f;
    }

    [SerializeField]
    private Transform _cameraTransform;
    [SerializeField]
    private Vector2 _cameraOffset = Vector2.zero;
    [SerializeField]
    public Material _material;
    [SerializeField]
    private int _sortingOrder;
    [HideInInspector]
    public RectTransform rectTransform;
    [SerializeField]
    public LensFlaresPreset _presets;
    public Color32 color;
    [HideInInspector]
    public float distance;
    //[HideInInspector]
    public Texture2D renderTexture;
    [HideInInspector]
    public int update;
    [HideInInspector]
    public AsyncGPUReadbackRequest request;
    public OcclusionCamera camera;
    public float min;
    public float max;
    public float divider;

    private List<Flare> _flares;

    public void AwakeW()
    {
        rectTransform = GetComponent<RectTransform>();
        CreateFlares();
        SubscribePresets();
    }

    private void OnDestroy()
    {
        UnsubscribePresets();
    }

    private void OnValidate()
    {
#if UNITY_EDITOR
        if (EditorApplication.isPlaying)
        {
            RecreateFlares();
        }
#endif
    }

    public void UpdateW()
    {
        update = GameManager.instance.DAT_28;

        if (Mathf.Abs(rectTransform.anchoredPosition.x) > LevelManager.instance.defaultCamera.pixelWidth / 2 ||
            Mathf.Abs(rectTransform.anchoredPosition.y) > LevelManager.instance.defaultCamera.pixelHeight / 2
            || rectTransform.anchoredPosition3D.z < 0)
        {
            if (gameObject.activeSelf)
            {
                gameObject.SetActive(false);

                if (UIManager.instance.cameras.Contains(camera))
                    UIManager.instance.cameras.Remove(camera);
            }

            return;
        }
        else
        {
            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
            }

            if (!UIManager.instance.cameras.Contains(camera))
                UIManager.instance.cameras.Add(camera);
        }

        if (SystemInfo.supportsAsyncGPUReadback)
                UIManager.instance.FUN_1D00C(this);

        UpdateFlares();
    }

    private void CreateFlares()
    {
        int count = _presets.presets.Length;

        if (_flares != null)
        {
            for (int i = 0, flareCount = _flares.Count; i < flareCount; ++i)
            {
                _flares[i].Clear();
            }
            _flares.Clear();
        }
        else
        {
            _flares = new List<Flare>(count);
        }

        for (int i = 0; i < count; ++i)
        {
            var flare = new Flare(this, i);
            _flares.Add(flare);
        }
    }

    private void RefreshFlares()
    {
        if (_flares == null)
            return;

        for (int i = 0, count = _flares.Count; i < count; ++i)
        {
            _flares[i].Refresh();
        }
    }

    private void RecreateFlares()
    {
        if (_flares == null)
            return;

        CreateFlares();
    }

    private void UpdateFlares()
    {
        for (int i = 0, count = _flares.Count; i < count; ++i)
        {
            _flares[i].Update();
        }
    }

    private void SubscribePresets()
    {
        if (_presets)
            _presets.onValidate += OnValidate;
    }

    private void UnsubscribePresets()
    {
        if (_presets)
            _presets.onValidate -= OnValidate;
    }
}
