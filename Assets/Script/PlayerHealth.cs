using System.Collections;
using System.Collections.Generic;
using Photon.Bolt;
using UnityEngine;
using TMPro;

public class PlayerHealth : Photon.Bolt.EntityBehaviour<IPlayerState>
{
    public int localHealth ;
   // public TextMeshProUGUI HealthPanel;

    public override void Attached()
    {
        //HealthPanel.enabled = false;
        state.Health = localHealth;
                    
       state.AddCallback("Health" , HealthCallBack);
        
       
    }

    private void HealthCallBack()
    {
        localHealth=state.Health;
        if (state.Health <= 0)
        {
            BoltNetwork.Destroy(gameObject); 
        }
    }
    /*
    public override void SimulateOwner()
    {
        if (entity.IsOwner)
        {
            HealthPanel.enabled = true;
            var evnt = PlayerAmmo.Create();
            if (state.Health > 0)
            {
                evnt.Ammo = "Ammo: " + (state.Health.ToString());
                evnt.Send();
                HealthPanel.text = evnt.Ammo;

            }
            if (state.Health <= 0)
            {
                evnt.Ammo = "Ammo: 0";
                evnt.Send();
                HealthPanel.text = evnt.Ammo;

            }
        }
    }
    */





    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(10);
    }


    private void OnTriggerEnter(Collider col)
    {
        
        if (col.gameObject.CompareTag("Zombie"))
        {
            
            Debug.Log("ölüyozzz");
            state.Health -= 1;
            
        }

    }

   
   
}
