using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/LensFlaresPreset")]
public class LensFlaresPreset : ScriptableObject
{
    public event Action onValidate;

    [SerializeField]
    private LensFlares.FlarePreset[] _presets;

    public LensFlares.FlarePreset[] presets
    {
        get { return _presets; }
        set { _presets = value; }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (onValidate != null)
            onValidate.Invoke();
    }
#endif
}
