using UnityEngine;
using UnityEngine.UI;

public class MapFrame : MonoBehaviour
{
    [SerializeField]
    private Color _highlight = Color.red;
    [SerializeField]
    private float _frequency = 1.0f;
    
    private Image _image;
    private Color _initColor;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _initColor = _image.color;
    }

    private void Update()
    {
        float wave = Mathf.Sin(Time.time * _frequency) * 0.5f + 0.5f;
        var color = Color.Lerp(_initColor, _highlight, wave);
        color.a = _initColor.a;
        _image.color = color;
    }
}
