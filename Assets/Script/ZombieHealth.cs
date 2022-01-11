using System.Collections;
using System.Collections.Generic;
using Photon.Bolt;
using UnityEngine;

public class ZombieHealth : Photon.Bolt.EntityBehaviour<IZombieState>
{
    
    public int localHealth;
    public HealthBarScript healthBar;
    

    public override void Attached()
    {
       
        state.HealthProperty = localHealth;
        healthBar.SetMax(localHealth);
        state.AddCallback("HealthProperty", HealthCallBack);
    }

    private void HealthCallBack()
    {
        localHealth = state.HealthProperty;

        if (state.HealthProperty <= 0)
        {
            var zombiecountmsg = ZombieCount.Create();

            BoltNetwork.Destroy(gameObject);

            zombiecountmsg.Dead = true;
            zombiecountmsg.Send();
            Debug.Log("zzombie health script : oldu");
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "BulletTrig")
        {
            state.HealthProperty -= 1;
            healthBar.SetHealth(localHealth);
        }
    }
    
}
