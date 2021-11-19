using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    [SerializeField]
    private Vector2 _factor = Vector2.one;

    private Transform _transform;
    private ParallaxController _owner;
    private Vector2 _oneMinusFactor;
    private bool _isFactorUseful;
#if UNITY_EDITOR
    private bool _isInformed;
#endif

    public Vector2 factor
    {
        get { return _factor; }
        set
        {
            _factor = value;
            RecalculateInnerFactor();
        }
    }

#if UNITY_EDITOR
    private void LateUpdate()
    {
        if (null == _owner && !_isInformed)
        {
            Debug.LogWarning("WunderCameraParallaxLayer is not linked with WunderCameraParallax.", this);
            _isInformed = true;
        }
    }

    private void OnValidate()
    {
        RecalculateInnerFactor();
    }
#endif

    public void Link(ParallaxController owner)
    {
        _owner = owner;
        _transform = transform;
        RecalculateInnerFactor();
    }

    public void Unlink()
    {
        _owner = null;
    }

    public void Apply(Vector2 cameraOffset)
    {
        if (_isFactorUseful)
        {
            // this matches GetOffset
            // but inlined manually for the sake of optimization
            Vector3 offset = Vector2.Scale(cameraOffset, _oneMinusFactor);
            _transform.position += offset;
        }
    }

    private void RecalculateInnerFactor()
    {
        const float kFactorSqrEpsilon = 0.000001f;
        _oneMinusFactor = Vector2.one - _factor;
        _isFactorUseful = (_oneMinusFactor.sqrMagnitude > kFactorSqrEpsilon);
    }
}
