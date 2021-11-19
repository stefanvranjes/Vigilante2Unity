using UnityEngine;

public class Tree : MonoBehaviour
{
    private static readonly Vector3 kForward = Vector3.forward;

    [SerializeField]
    private float _minAmplitude = 1.0f;
    [SerializeField]
    private float _maxAmplitude = 4.0f;
    [SerializeField]
    private float _minSpeed = 1.0f;
    [SerializeField]
    private float _maxSpeed = 2.0f;

    private Transform _transform;
    private float _amplitude;
    private float _frequency;
    private float _offset;

    public void Init()
    {
        _transform = transform;
        _amplitude = Random.Range(_minAmplitude, _maxAmplitude);
        float speed = Random.Range(_minSpeed, _maxSpeed);
        _frequency = speed / _amplitude;
        _offset = Random.Range(0.0f, 2.0f * Mathf.PI);
    }

    public void Swing(float time)
    {
        float angle = Mathf.Sin(time * _frequency + _offset) * _amplitude;
        _transform.localRotation = Quaternion.AngleAxis(angle, kForward);
    }
}
