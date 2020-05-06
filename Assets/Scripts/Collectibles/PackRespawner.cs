
using UnityEngine;
using System.Collections.Generic;

public class PackRespawner : MonoBehaviour
{
    class Pending
    {
        public GameObject go;
        public float timeLeft;
    }

    List<Pending> pending = new List<Pending>();
    public float respawnTime;

    public static PackRespawner instance { get; private set; }

    void Awake()
    {
        instance = this;
    }

    public void Add(GameObject go)
    {
        go.SetActive(false);
        var pendingObj = new Pending();
        pendingObj.go = go;
        pendingObj.timeLeft = respawnTime;
        pending.Add(pendingObj);
    }

    void Update()
    {
        int n = pending.Count;
        while (n-- > 0) {
            pending[n].timeLeft -= Time.deltaTime;
            if (pending[n].timeLeft <= 0.0f) {
                pending[n].go.SetActive(true);
                pending.RemoveAt(n);
            }
        }
    }
}
