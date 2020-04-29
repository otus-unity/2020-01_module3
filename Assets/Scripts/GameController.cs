using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Transform SpawnPoint;
    public GameObject Prefab;

    void Start()
    {
        GameObject prefab = PhotonNetwork.Instantiate(Prefab.name, SpawnPoint.position, Quaternion.identity);
        prefab.AddComponent<PlayerMovement>();
    }
}
