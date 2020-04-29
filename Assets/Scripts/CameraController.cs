using UnityEngine;


public sealed class CameraController : MonoBehaviour
{
    private const string MOUSE_SCROLL_WHEEL = "Mouse ScrollWheel";
    private const string MOUSE_X = "Mouse X";
    private const string MOUSE_Y = "Mouse Y";
    private Transform _target;
    [SerializeField] private CursorLockMode _cursorLockMode;

    [Header("LookAt")]
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _zoomSpeed = 4.0f;
    [SerializeField] private float _minZoom = 5.0f;
    [SerializeField] private float _maxZoom = 15.0f;
    [SerializeField] private float _pitch = 2.0f;
    private float _currentZoom = 0.0f;
    private float _currentRot = 0.0f;
    private float _prevMouseX;


    [Space(5), Header("RotationCamera")]
    [SerializeField] private float _distance = 2.0f;
    [SerializeField] private float _height = 1.5f;

    [SerializeField] private float _offsetPosition;

    [SerializeField] private float _minY = 15.0f;
    [SerializeField] private float _maxY = 15.0f;

    [SerializeField] private float _speed = 8.0f;
    private float _rotationY;
    private float _sensitivity = 2.0f;

    private void Start()
    {
        Cursor.lockState = _cursorLockMode;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private void Update()
    {
        _currentZoom -= Input.GetAxis(MOUSE_SCROLL_WHEEL) * _zoomSpeed;
        _currentZoom = Mathf.Clamp(_currentZoom, _minZoom, _maxZoom);

        if (Input.GetMouseButton(2))
        {
            _currentRot += Input.mousePosition.x - _prevMouseX;
        }
        _prevMouseX = Input.mousePosition.x;
    }

    private void LateUpdate()
    {
        RotationCamera();
    }

    private void RotationCamera()
    {
        if (_target == null)
            return;

        transform.RotateAround(_target.position, Vector3.up, Input.GetAxis(MOUSE_X) * _sensitivity);

        Vector3 position = _target.position - (transform.rotation * Vector3.forward * _distance);
        position = position + (transform.rotation * Vector3.right * _offsetPosition);
        position = new Vector3(position.x, _target.position.y + _height, position.z);

        _rotationY += Input.GetAxis(MOUSE_Y) * _sensitivity;
        _rotationY = Mathf.Clamp(_rotationY, -Mathf.Abs(_minY), Mathf.Abs(_maxY));
        transform.localEulerAngles = new Vector3(_rotationY, transform.localEulerAngles.y, 0);

        transform.position = Vector3.Lerp(transform.position, position, _speed * Time.deltaTime);
    }

    private void LookAt()
    {
        transform.position = _target.position - _offset * _currentZoom;
        transform.LookAt(_target.position + Vector3.up * _pitch);
        transform.RotateAround(_target.position, Vector3.up, _currentRot);
    }
}
