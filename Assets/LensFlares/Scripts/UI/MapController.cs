using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(GridLayoutGroup))]
public class MapController : MonoBehaviour
{
    private const int kEstimatedMapCount = 14;
    private static readonly int kBiasPropID = Shader.PropertyToID("_Bias");

    private struct Map
    {
        public GameObject gameObject;
        public Material material;
    }

    [SerializeField]
    private RenderTexture _texture;
    [SerializeField]
    private GameObject _mapPrefab;
    [SerializeField]
    private float _maxMapSize = 64.0f;

    private List<Map> _maps = new List<Map>(kEstimatedMapCount);
    private RectTransform _rectTransform;
    private GridLayoutGroup _layout;
    private int _prevWidth, _prevHeight;

    private void Start()
    {
        _rectTransform = transform as RectTransform;
        _layout = GetComponent<GridLayoutGroup>();
        Reinit();
    }

#if UNITY_EDITOR
    private void LateUpdate()
    {
        if (_texture.width != _prevWidth || _texture.height != _prevHeight)
        {
            Clear();
            Reinit();
        }
    }
#endif

    private void Clear()
    {
        for (int i = 0, count = _maps.Count; i < count; ++i)
        {
            var map = _maps[i];
            Destroy(map.gameObject);
        }
        _maps.Clear();
    }

    private void Reinit()
    {
        _prevWidth = _texture.width;
        _prevHeight = _texture.height;
        int maxSize = Mathf.Max(_prevWidth, _prevHeight);
        int maxLevel = Mathf.CeilToInt(Mathf.Log(maxSize, 2.0f)) + 1;

        LayoutRebuilder.ForceRebuildLayoutImmediate(_rectTransform);
        float totalHeight = _rectTransform.rect.height;
        float totalSpacing = _layout.spacing.y * (maxLevel - 1);
        float cellSize = Mathf.Min(Mathf.Floor((totalHeight - totalSpacing) / maxLevel), _maxMapSize);
        _layout.cellSize = new Vector2(cellSize, cellSize);

        for (int i = 0; i < maxLevel; ++i)
        {
            bool isImportant = i == maxLevel - 1;
            var map = Instantiate(_mapPrefab, i, isImportant);
            _maps.Add(map);
        }
    }

    private Map Instantiate(GameObject prefab, int index, bool isImportant)
    {
        var map = Instantiate(prefab, _rectTransform);
        var image = map.GetComponentInChildren<RawImage>();
        var material = new Material(image.material);
        image.texture = _texture;
        image.material = material;
        material.SetFloat(kBiasPropID, index);
        var text = map.GetComponentInChildren<Text>();
        text.text = $"{index}";
        var frame = map.GetComponentInChildren<MapFrame>();
        frame.enabled = isImportant;
#if UNITY_EDITOR
        image.name = $"{prefab.name}_{index}";
#endif
        return new Map()
        {
            gameObject = map,
            material = material
        };
    }
}
