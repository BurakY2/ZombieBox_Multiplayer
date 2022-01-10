using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Bolt;

public class Bullet : Photon.Bolt.EntityBehaviour<IBulletState>
{
    public override void Attached()
    {
        state.SetTransforms(state.BulletTransform, gameObject.transform);

    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "Player")
        {
            Destroy(gameObject);
        }


    }
}
