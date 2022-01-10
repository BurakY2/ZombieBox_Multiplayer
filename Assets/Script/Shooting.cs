using System.Collections;
using System.Collections.Generic;
using Photon.Bolt;
using UnityEngine;

public class Shooting : Photon.Bolt.EntityBehaviour<IPlayerState>
{
    public Camera camera;
    public GameObject projectile;
    private Vector3 destination;
    public Transform firePoint;
    public int ammo ;
    public float projectileSpeed ;
    private bool isReloading = false;

    public override void Attached()
    {
        state.OnShooting = aim;
    }

    void Update()
    {
        if (isReloading)
        {
            return;
        }
        if(ammo<=0)
        {
            StartCoroutine(reload());
            ammo = 3;
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
}
