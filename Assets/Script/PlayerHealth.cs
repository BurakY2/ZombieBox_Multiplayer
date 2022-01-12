using System.Collections;
using System.Collections.Generic;
using Photon.Bolt;
using UnityEngine;
using TMPro;

public class PlayerHealth : Photon.Bolt.EntityBehaviour<IPlayerState>
{
    public int localHealth ;
    public TextMeshProUGUI HealthPanel;
    public GameObject canvas;
    public TextMeshProUGUI winnerPopup;
    public CharacterController a;
    public TextMeshProUGUI ammopanel;
    public Renderer rend;
    public GameObject arm;
   



    public override void Attached()
    {

        state.SetTransforms(state.Arm, arm.transform);
        rend = GetComponent<Renderer>();
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
                HealthPanel.enabled = false;
                losepanel();
                a.enabled = false;
                this.gameObject.tag = "Death";
                
                this.gameObject.GetComponent<Shooting>().enabled = false;
                rend.enabled = false;
                arm.SetActive(false);
                //arm.SetActive(false);
                Destroy(this.gameObject);
                /*
                evnt.HealthMsg = "Health: 0";
                HealthPanel.text = evnt.HealthMsg;
                
                evnt.Send();
                
                gameoverevent.Lose = true;
                gameoverevent.Send();
                */

                //BoltNetwork.Destroy(player.GetComponent<PlayerMove>());
                Debug.Log("oLDUN");

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
            if (entity.IsOwner)
            {
                HealthPanel.enabled = false;
                losepanel();
                a.enabled = false;
                this.gameObject.tag = "Death";
                this.gameObject.GetComponent<Shooting>().enabled = false;
                rend.enabled = false;
                arm.SetActive(false);

            }
            
            //BoltNetwork.Destroy(gameObject);
        }

    }


    public void losepanel()
    {
        TextMeshProUGUI winnerPopupClone = Instantiate(winnerPopup);
        winnerPopupClone.text = "You Dead";
        winnerPopupClone.transform.SetParent(canvas.transform);
        winnerPopupClone.GetComponent<RectTransform>().sizeDelta = winnerPopup.GetComponent<RectTransform>().sizeDelta;
        winnerPopupClone.GetComponent<RectTransform>().localScale = winnerPopup.GetComponent<RectTransform>().localScale;
        winnerPopupClone.GetComponent<RectTransform>().position = winnerPopup.GetComponent<RectTransform>().position;
    }
   
   
}
