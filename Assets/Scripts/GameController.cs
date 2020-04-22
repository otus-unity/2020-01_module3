using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject Prefab;

    void Start()
    {
        GameObject prefab = PhotonNetwork.Instantiate(Prefab.name, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        prefab.AddComponent<PlayerMovement>();
    }
}
