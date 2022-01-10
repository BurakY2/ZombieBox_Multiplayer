using System.Collections;
using System.Collections.Generic;
using Photon.Bolt;
using UnityEngine;

public class PlayerHealth : Photon.Bolt.EntityBehaviour<IPlayerState>
{
    public int localHealth ;

    public override void Attached()
    {
        
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

    private void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "BulletTrig") 
        {
                state.Health -= 1;
        }
    }
   
}
