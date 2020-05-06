using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;


namespace DefaultNamespace
{
    public sealed class PlayerHP : MonoBehaviourPunCallbacks, IPunObservable
    {
        public float MaxHealth = 100.0f;
        public TextMesh HealthBar;
        public float Health => _health;
        private float _health;

        private void Start()
        {
            _health = MaxHealth;
            GetDamageRPC(0.0f);
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(_health);
            }
            else
            {
                _health = (float)stream.ReceiveNext();
            }
        }

        [PunRPC]
        private void GetDamageRPC(float damage)
        {
            _health -= damage;
            HealthBar.text = _health <= 0.0f ? "0" : $"{_health}";
            if (_health <= 0.0f && photonView.IsMine)
            {
                RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
                SendOptions sendOptions = new SendOptions { Reliability = true };

                PhotonNetwork.RaiseEvent(1, gameObject.name, raiseEventOptions, sendOptions);
            }
        }

        public void AddHealth(float add)
        {
            GetDamageRPC(-add);
        }
    }
}
