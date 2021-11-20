using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/OcclusionPreset")]
public class OcclusionPreset : ScriptableObject
{
    [SerializeField]
    private OcclusionCamera.OccludePresest[] _presets;

    public OcclusionCamera.OccludePresest[] presets
    {
        get { return _presets; }
        set { _presets = value; }
    }
}
