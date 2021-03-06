using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Bolt;
using UnityEngine.AI;

public class ZombieMovement : EntityBehaviour<IZombieState>
{

    
    public NavMeshAgent agent;
    public GameObject[] player;
    public float konum = 1000f;
    public GameObject[] player2;




    public void Start()
    {
        
        player = GameObject.FindGameObjectsWithTag("Player");
    }


    public override void Attached()
    {
        state.SetTransforms(state.ZombieTransform, transform);



    }

    public void Update()
    {

        
            agent = GetComponent<NavMeshAgent>();

        /*
        Transform a = player[0].transform;
        Debug.Log("player0: " + a.position.x);
        Transform b = player[1].transform;
        Debug.Log("player1: " + b.position.x);

        */

        try
        {
            foreach (GameObject enemy in player)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < konum)
                {

                    agent.destination = enemy.transform.position;
                }


            }

        }catch(MissingReferenceException e)
        {
            player2 = GameObject.FindGameObjectsWithTag("Player");

            foreach (GameObject enemy in player2)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < konum)
                {

                    agent.destination = enemy.transform.position;
                }


            }


        }
           
            









            /*
            

            if (a.position.x > b.position.x && !player[0].CompareTag("Death") && !player[1].CompareTag("Death"))
            {
                if (a.position.y > b.position.y && !player[0].CompareTag("Death") && !player[1].CompareTag("Death"))
                {
                    agent.destination = player[1].transform.position;
                }
                else
                {
                    agent.destination = player[0].transform.position;
                }

            }
            else
            {
                if (a.position.y < b.position.y && !player[0].CompareTag("Death") && !player[1].CompareTag("Death"))
                {
                    agent.destination = player[0].transform.position;
                }
                if (a.position.y > b.position.y && !player[0].CompareTag("Death") && !player[1].CompareTag("Death"))
                {
                    agent.destination = player[1].transform.position;
                }
                if (player[1].CompareTag("Death") || player[0] == null)
                {
                    agent.destination = player[0].transform.position;
                }
                if (player[0].CompareTag("Death") || player[1] == null)
                {
                    agent.destination = player[1].transform.position;
                }
                else
                {
                    agent.destination = player[1].transform.position;
                }

            }

        }
        catch (MissingReferenceException e)
        {


        }
            */
     

    }

    public override void SimulateOwner()
    {


    }
}
