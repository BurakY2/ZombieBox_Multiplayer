using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Bolt;

public class NetworkCallback : GlobalEventListener
{
    
    float b = 2.3871f;
    public GameObject PlayerPreFab;
    public GameObject zombiePrefab;


    void Start()
    {
        //Start the coroutine we define below named ExampleCoroutine.
        StartCoroutine(ExampleCoroutine());
    }


    IEnumerator ExampleCoroutine()
    {
        Debug.Log("Started Coroutine at timestamp : " + Time.time);
        yield return new WaitForSeconds(10);
        if (BoltNetwork.IsServer)
        {
            for (int i = 0; i < 10; i++)
            {
                spamBots();
            }
        }
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }



    public override void SceneLoadLocalDone(string scene, IProtocolToken token)
    {
        var spawnPos = new Vector3(Random.Range(10, 27), 2f, Random.Range(-49, -50));

        if (BoltNetwork.IsServer)
        {
            BoltNetwork.Instantiate(PlayerPreFab, spawnPos, Quaternion.identity);
        }
        else
        {
            BoltNetwork.Instantiate(PlayerPreFab, spawnPos, Quaternion.identity);
        }
    }



    public void spamBots()
    {
        Debug.Log("Spam bots");
        var spawnPos = new Vector3(Random.Range(1, 38), b, Random.Range(40, 48));
        BoltNetwork.Instantiate(zombiePrefab, spawnPos, Quaternion.identity);


    }





}
