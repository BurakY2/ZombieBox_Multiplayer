using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Bolt;

public class NetworkCallback : GlobalEventListener
{
    float a = 0.395f;
    float b = 1.41f;
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
        var spawnPos = new Vector3(Random.Range(30, 40), 1f, Random.Range(-58, -40));

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
        var spawnPos = new Vector3(Random.Range(-18, 9), b, Random.Range(13, 25));
        BoltNetwork.Instantiate(zombiePrefab, spawnPos, Quaternion.identity);


    }





}
