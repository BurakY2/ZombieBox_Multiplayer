using System.Collections;
using System.Collections.Generic;
using Photon.Bolt;
using UnityEngine;
using TMPro;

public class Shooting : Photon.Bolt.EntityBehaviour<IPlayerState>
{
    
    public Camera camera;
    public GameObject projectile;
    private Vector3 destination;
    public Transform firePoint;
    public int ammo ;
    public float projectileSpeed ;
    private bool isReloading = false;
    public TextMeshProUGUI AmmoPanel;


    public override void Attached()
    {
        
        state.OnShooting = aim;
        AmmoPanel.enabled = false;




    }
    public override void SimulateOwner()
    {
        if (entity.IsOwner)
        {
            AmmoPanel.enabled = true;
            var evnt = PlayerAmmo.Create();
            if (ammo > 0)
            {
                evnt.Ammo = (ammo.ToString());
                evnt.Send();
                AmmoPanel.text = evnt.Ammo;

            }
            if (ammo <= 0)
            {
                evnt.Ammo = "0";
                evnt.Send();
                AmmoPanel.text = evnt.Ammo;

            }
        }
    }

    void Update()
    {

        

       
        
        if (isReloading)
        {
            return;
        }
        if(ammo<=0)
        {
            ammo = 0;
            StartCoroutine(reload()); 
            
        }

        if (ammo > 0)
        {
            if(Input.GetButtonDown("Fire1") && entity.IsOwner)
            {
                
                ammo--;
                state.Shooting();
            }
        }
         
    }

    IEnumerator reload()
    {
        isReloading = true;
        Debug.Log("reloading");
        yield return new WaitForSeconds(1f);
        ammo = 3;
        isReloading = false;
    }

    public void aim()
    {
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit rayHit;
        if (Physics.Raycast(ray, out rayHit))
        {
            destination = rayHit.point;
        }
        else
        {
            destination = ray.GetPoint(1000);
        }
        shootProjectile(destination);
    }
    
    public void shootProjectile(Vector3 target )
    {
        var projectileObj = Instantiate(projectile,firePoint.position,Quaternion.identity) as GameObject;
        projectileObj.GetComponent<Rigidbody>().velocity = (target - firePoint.position).normalized * projectileSpeed;
    }


    public void AmmoPanelText()
    {
        
        
       
    
    }
}
