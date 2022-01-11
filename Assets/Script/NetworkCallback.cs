using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Bolt;

public class NetworkCallback : GlobalEventListener
{
    float a = 0.395f;
    public GameObject PlayerPreFab;
    public GameObject zombiePrefab;

    public override void SceneLoadLocalDone(string scene, IProtocolToken token)
    {
        var spawnPos = new Vector3(Random.Range(-8, 8), a, Random.Range(-8, 8));

        if (BoltNetwork.IsServer)
        {
            BoltNetwork.Instantiate(PlayerPreFab, spawnPos, Quaternion.identity);
        }
        else
        {
            BoltNetwork.Instantiate(PlayerPreFab, spawnPos, Quaternion.identity);
        }
        BoltNetwork.Instantiate(zombiePrefab, spawnPos, Quaternion.identity);

    }
}
