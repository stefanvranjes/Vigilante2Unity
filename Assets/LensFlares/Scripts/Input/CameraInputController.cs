using UnityEngine;

public class CameraInputController : MonoBehaviour
{
    [SerializeField]
    private Vector2 _sensitivity = Vector2.one;
    [SerializeField]
    private Vector2 _damping = Vector2.one;
    [SerializeField]
    private Rect _limit = new Rect(-6.0f, -0.8f, 12.0f, 1.2f);

    private Transform _transform;
    private Vector2 _prevPosition;
    private Vector2 _velocity;
    private bool _isPanning;

    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        if (Input.touchSupported && Application.platform != RuntimePlatform.WebGLPlayer)
            HandleTouch();
        else
            HandleMouse();
        PanCamera();
    }

    private void HandleMouse()
    {
        var prevIsPanning = _isPanning;
        _isPanning = Input.GetMouseButton(0);
        if (_isPanning)
        {
            if (!prevIsPanning)
            {
                _prevPosition = Input.mousePosition;
            }
            Vector2 position = Input.mousePosition;
            Vector2 offset = position - _prevPosition;
            _velocity -= offset * Time.deltaTime;
            _prevPosition = position;
        }
    }

    private void HandleTouch()
    {
        _isPanning = Input.touchCount == 1;
        if (_isPanning)
        {
            var touch = Input.GetTouch(0);
            Vector2 offset = touch.deltaPosition;
            _velocity -= offset * Time.deltaTime;
        }
    }

    private void PanCamera()
    {
        var translation = Vector2.Scale(_velocity, _sensitivity);
        _transform.Translate(translation, Space.World);
        _velocity = Vector2.Scale(_velocity, _damping);
        LimitCamera();
    }

    private void LimitCamera()
    {
        var position = _transform.position;
        position.x = Mathf.Clamp(position.x, _limit.xMin, _limit.xMax);
        position.y = Mathf.Clamp(position.y, _limit.yMin, _limit.yMax);
        _transform.position = position;
    }
}
