using Photon.Pun;
using UnityEngine;

public sealed class Gun : MonoBehaviour
{
    public float Damage = 2.0f;
    private PlayerAnimation _playerAnimation;
    private Camera _mainCamera;
    private Vector2 _center;
    private readonly float _dedicateDistance = 20.0f;
    private int _layerMask;
    private bool _isReady = true;
    private float _rechergeTime = 0.2f;

    public void Start()
    {
        _mainCamera = Camera.main;
        _layerMask = 0;//1 << 8;
        _layerMask = ~ _layerMask;
        _center.Set(Screen.width / 2.0f, Screen.height / 2.0f);
    }

    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

    public void SetPlayerAnimation(PlayerAnimation playerAnimation)
    {
        _playerAnimation = playerAnimation;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (_isReady)
            {
                if (Physics.Raycast(_mainCamera.ScreenPointToRay(_center),
                    out RaycastHit hit, _dedicateDistance, _layerMask))
                {
                    if (hit.collider)
                    {
                        if (hit.collider.TryGetComponent<PhotonView>(out PhotonView view))
                        {
                            view.RPC("GetDamageRPC", RpcTarget.All, Damage);
                            _isReady = false;
                            Invoke(nameof(ReadyShoot), _rechergeTime);
                        }
                    }
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            _center.Set(Screen.width / 2.0f, Screen.height / 2.0f);
            _playerAnimation.OnFireEnable();
        }
        if (Input.GetMouseButtonUp(0))
        {
            _playerAnimation.OnFireDisable();
        }
    }

    private void ReadyShoot()
    {
        _isReady = true;
    }
}
