using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Bolt;
using TMPro;

public class GameOver : GlobalEventListener
{
    public Camera MainCamera;
    public GameObject canvas;
    public TextMeshProUGUI winnerPopup;

    public override void OnEvent(GameOverEvent evnt)
    {
        Debug.Log(evnt.Lose);
       if(evnt.Lose == true)
        {
            TextMeshProUGUI winnerPopupClone = Instantiate(winnerPopup);
            winnerPopupClone.text = "You Lose";

            
            winnerPopupClone.transform.SetParent(canvas.transform);
            winnerPopupClone.GetComponent<RectTransform>().sizeDelta = winnerPopup.GetComponent<RectTransform>().sizeDelta;
            winnerPopupClone.GetComponent<RectTransform>().localScale = winnerPopup.GetComponent<RectTransform>().localScale;
            winnerPopupClone.GetComponent<RectTransform>().position = winnerPopup.GetComponent<RectTransform>().position;

        }
       if(evnt.Lose == false)
        {

            TextMeshProUGUI winnerPopupClone = Instantiate(winnerPopup);
            winnerPopupClone.text = "You Win";


            winnerPopupClone.transform.SetParent(canvas.transform);
            winnerPopupClone.GetComponent<RectTransform>().sizeDelta = winnerPopup.GetComponent<RectTransform>().sizeDelta;
            winnerPopupClone.GetComponent<RectTransform>().localScale = winnerPopup.GetComponent<RectTransform>().localScale;
            winnerPopupClone.GetComponent<RectTransform>().position = winnerPopup.GetComponent<RectTransform>().position;

        }

    }
}
