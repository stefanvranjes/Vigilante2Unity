using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    private List<Tree> _trees = new List<Tree>();

    private void Awake()
    {
        GetComponentsInChildren(_trees);
        for (int i = 0, count = _trees.Count; i < count; ++i)
        {
            _trees[i].Init();
        }
    }

    private void Update()
    {
        var time = Time.time;
        for (int i = 0, count = _trees.Count; i < count; ++i)
        {
            _trees[i].Swing(time);
        }
    }
}
