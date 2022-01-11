using System.Collections;
using System.Collections.Generic;
using Photon.Bolt;
using UnityEngine;
using TMPro;

public class PlayerHealth : Photon.Bolt.EntityBehaviour<IPlayerState>
{
    public int localHealth ;
    public TextMeshProUGUI HealthPanel;
   
    



    public override void Attached()
    {
        HealthPanel.enabled = false;
        state.Health = localHealth;
        state.AddCallback("Health", HealthCallBack);
       

    }


    public override void SimulateOwner()
    {
        
        
    }


    private void HealthCallBack()
    {
        
        localHealth=state.Health;
        /*
        if (state.Health <= 0)
        {
            BoltNetwork.Destroy(gameObject); 
        }
        */

        if (entity.IsOwner)
        {
            HealthPanel.enabled = true;
            var evnt = PlayerHealthEvent.Create();
            var gameoverevent = GameOverEvent.Create();
            if (state.Health > 0)
            {
                evnt.HealthMsg = "Health: " + (state.Health.ToString());
                evnt.Send();
                HealthPanel.text = evnt.HealthMsg;

            }
            if(state.Health == 0)
            {
                evnt.HealthMsg = "Health: 0";
                HealthPanel.text = evnt.HealthMsg;
                HealthPanel.enabled = false;
                evnt.Send();
                
                gameoverevent.Lose = true;
                gameoverevent.Send();

            }
            if (state.Health <= -1)
            {
                
                evnt.HealthMsg = "Health: 0";
                evnt.Send();
                HealthPanel.text = evnt.HealthMsg;
                gameoverevent.Lose = true;
                gameoverevent.Send();

                BoltNetwork.Destroy(gameObject);

            }
        }

    }
    


    private void OnTriggerEnter(Collider col)
    {
        
        if (col.gameObject.CompareTag("Zombie"))
        {
            
            
            state.Health -= 1;
            
        }
        if (col.gameObject.CompareTag("Pool"))
        {
            BoltNetwork.Destroy(gameObject);
        }

    }

   
   
}
