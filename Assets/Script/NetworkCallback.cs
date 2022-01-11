using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Bolt;

public class NetworkCallback : GlobalEventListener
{
    
    float b = 2.3871f;
    public GameObject PlayerPreFab;
    public GameObject zombiePrefab;
    public int zombiecounter= 0;

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
            for (int i = 0; i < 2; i++)
            {
                spamBots();
                zombiecounter++;

            }
        }
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }
    public override void OnEvent(ZombieCount evnt)
    {
        Debug.Log(evnt.Dead);
        if (evnt.Dead)
        {
            zombiecounter--;
            if(zombiecounter == 0)
            {
                var gameoverevent = GameOverEvent.Create();
                gameoverevent.Lose = false;
                gameoverevent.Send();

            }
            
        }
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
