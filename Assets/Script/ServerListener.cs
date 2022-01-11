using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Bolt;

public class ServerListener : GlobalEventListener
{
    public override void OnEvent(PlayerJoinedEvent evnt)
    {
        Debug.Log(evnt.Message);
    }
}

