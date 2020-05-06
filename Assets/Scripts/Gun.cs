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
    GunAmmo ammo;
    bool isBot;

    public void Start()
    {
        ammo = GetComponentInParent<GunAmmo>();
        isBot = GetComponent<BotUtility>() != null;
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

    public bool HasEnoughAmmo()
    {
        return ammo.Count > 0;
    }

    public void BeginAnimateShoot()
    {
        _playerAnimation.OnFireEnable();
    }

    public void EndAnimateShoot()
    {
        _playerAnimation.OnFireDisable();
    }

    public bool Shoot(Ray ray)
    {
        --ammo.Count;
        foreach (var hit in Physics.RaycastAll(ray, _dedicateDistance, _layerMask))
        {
            if (hit.collider)
            {
                if (hit.collider.TryGetComponent<PhotonView>(out PhotonView view))
                {
                    view.RPC("GetDamageRPC", RpcTarget.All, Damage);
                    return true;
                }
            }
        }
        return false;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && HasEnoughAmmo())
        {
            if (_isReady)
            {
                Shoot(_mainCamera.ScreenPointToRay(_center));
                _isReady = false;
                Invoke(nameof(ReadyShoot), _rechergeTime);
            }
        }

        if (Input.GetMouseButtonDown(0) && HasEnoughAmmo())
        {
            _center.Set(Screen.width / 2.0f, Screen.height / 2.0f);
            BeginAnimateShoot();
        }
        if (Input.GetMouseButtonUp(0))
        {
            EndAnimateShoot();
        }
    }

    private void ReadyShoot()
    {
        _isReady = true;
    }
}
