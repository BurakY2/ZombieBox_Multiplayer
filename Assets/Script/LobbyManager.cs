using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Bolt;
using Photon.Bolt.Matchmaking;
using UdpKit;
using System;

public class LobbyManager : GlobalEventListener
{
    public Button joinGameButtonPrefab;
    public GameObject ServerListPanel;
    public GameObject setUsernamePanel;
    public float buttonSpacing;
    private List<Button> joinServerButtons = new List<Button>();

    public void StartServer()
    {
        BoltLauncher.StartServer();
    }

    public override void BoltStartDone()
    {
        //BoltMatchmaking.CreateSession("Room1", sceneToLoad:"GameScene");
        int randomInt = UnityEngine.Random.Range(0, 9999);
        string matchName = "Room1" + randomInt;
        BoltMatchmaking.CreateSession(sessionID: matchName, sceneToLoad: "GameScene");
    }

    public void StartClient()
    {
        BoltLauncher.StartClient();
    }

    public override void SessionListUpdated(Map<Guid, UdpSession> sessionList)
    {
        ClearSessions();

        int totalsessions = 0;
        print("totalSessions initial:" + totalsessions);


        foreach (var session in sessionList)
        {
            UdpSession photonSession = session.Value as UdpSession;

            Button joinGameButtonClone = Instantiate(joinGameButtonPrefab);
            joinGameButtonClone.transform.parent = ServerListPanel.transform;
            joinGameButtonClone.transform.localPosition = new Vector3(0, buttonSpacing * joinServerButtons.Count, 0);
            joinGameButtonClone.gameObject.SetActive(true);
            joinGameButtonClone.onClick.AddListener(() => JoinGame(photonSession));

            joinServerButtons.Add(joinGameButtonClone);

        }


        /*
        foreach (var session in sessionList)
        {
            UdpSession photonSession = session.Value as UdpSession;
            if(photonSession.Source == UdpSessionSource.Photon)
            {
                BoltMatchmaking.JoinSession(photonSession);
            }
        }
        */
    }



    private void JoinGame(UdpSession photonSession)
    {
        BoltMatchmaking.JoinSession(photonSession);
    }


    private void ClearSessions()
    {
        Debug.Log("Clear sessions call");
        foreach (Button button in joinServerButtons)
        {
            Destroy(button.gameObject);
        }

        joinServerButtons.Clear();
    }
}
