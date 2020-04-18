using UnityEngine;
using UnityEngine.AI;


public sealed class PlayerMovement : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";
    [Range(0.0f, 1.0f), SerializeField] private float _moveSpeed;
    private Vector3 _movement;
    private PlayerAnimation _playerAnimation;
    private NavMeshAgent _agent;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
        _agent = GetComponent<NavMeshAgent>();
        _playerAnimation = GetComponent<PlayerAnimation>();
        FindObjectOfType<CameraController>().SetTarget(transform);
        _agent.updateRotation = false;
    }

    private void Update()
    {
        var timeDelta = Time.deltaTime;
        var horizontal = Input.GetAxis(HORIZONTAL);
        var vertical = Input.GetAxis(VERTICAL);

        if (Input.GetMouseButtonDown(0))
        {
            _playerAnimation.OnFireEnable();
        }
        if (Input.GetMouseButtonUp(0))
        {
            _playerAnimation.OnFireDisable();
        }

        _movement.Set(horizontal, 0.0f, vertical);

        if (Mathf.Abs(_movement.magnitude) > 1.0f)
        {
            _movement.Normalize();
        }

        Vector3 targetDirection = _camera.transform.TransformDirection(_movement);
        targetDirection.y = 0.0f;
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x,
            _camera.transform.localEulerAngles.y, transform.localEulerAngles.z);

        _agent.Move(targetDirection * (timeDelta * _moveSpeed));
        _agent.SetDestination(transform.position +  targetDirection);

        _playerAnimation.SetMove(_movement);
    }


    private void OnAnimatorMove()
    {
        if (_agent.velocity.magnitude > 0)
        {
            //_playerAnimation.Animator.speed = _agent.velocity.magnitude;
        }
    }
}
