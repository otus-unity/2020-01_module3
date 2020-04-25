using UnityEngine;
using UnityEngine.AI;


public sealed class PlayerMovement : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";
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
        gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void Update()
    {
        var timeDelta = Time.deltaTime;
        var horizontal = Input.GetAxis(HORIZONTAL);
        var vertical = Input.GetAxis(VERTICAL);

        _movement.Set(horizontal, 0.0f, vertical);

        if (Mathf.Abs(_movement.magnitude) > 1.0f)
        {
            _movement.Normalize();
        }

        Vector3 targetDirection = _camera.transform.TransformDirection(_movement);
        targetDirection.y = 0.0f;
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x,
            _camera.transform.localEulerAngles.y, transform.localEulerAngles.z);

        _agent.Move(targetDirection * timeDelta);
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
