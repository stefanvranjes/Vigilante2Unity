using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image), typeof(Toggle))]
public class ToggleImage : MonoBehaviour
{
    [SerializeField]
    private Sprite _onSprite;
    [SerializeField]
    private Sprite _offSprite;

    private Toggle _toggle;
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _toggle = GetComponent<Toggle>();
        _toggle.onValueChanged.AddListener(OnValueChanged);
        OnValueChanged(_toggle.isOn);
    }

    private void OnDestroy()
    {
        _toggle.onValueChanged.RemoveListener(OnValueChanged);
    }

    private void OnValueChanged(bool isOn)
    {
        _image.sprite = isOn ? _onSprite : _offSprite;
    }
}
