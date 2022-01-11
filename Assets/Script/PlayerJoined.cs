using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Bolt;

public class PlayerJoined : EntityBehaviour<IPlayerState>
{
    private string username;
    public override void Attached()
    {
        if (entity.IsOwner)
        {
            var evnt = PlayerJoinedEvent.Create();
            string a = PlayerPrefs.GetString("username");
            //PlayerPrefs.SetString("username", Random.Range(0, 9999).ToString());
            evnt.Message = a + " Hello there !";
            evnt.Send();
        }
    }
}
