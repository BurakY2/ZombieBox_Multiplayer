using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public Slider slider;
    public void SetMax(int hp)
    {
        slider.maxValue = hp;
        slider.value = hp;
    }

    public void SetHealth(int hp)
    {
        slider.value = hp;
    }
    

}
