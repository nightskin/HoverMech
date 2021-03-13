using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public Slider HP_Bar;
    public Slider Heat_Gauge;

    public float health;
    public float temperature;
    public bool overheating;
    public float cooling = 3;

    void Start()
    {
        health = 100;
        temperature = 0;
        overheating = false;
    }

    void Update()
    {
        if(temperature > 100)
        {
            temperature = 100;
        }
        else if(temperature < 0)
        {
            temperature = 0;
        }

        if(temperature <= 50)
        {
            Heat_Gauge.targetGraphic.color = Color.yellow;
        }
        else if(temperature > 50 && temperature < 75)
        {
            Heat_Gauge.targetGraphic.color = new Color(1, 0.5f, 0);
        }
        else if(temperature > 75)
        {
            Heat_Gauge.targetGraphic.color = Color.red;
        }


        if (temperature == 100)
        {
            StartCoroutine(CoolDown());
        }
        
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        HP_Bar.value = health;
    }

    public void Heal(float amount)
    {
        health += amount;
        HP_Bar.value = health;
    }

    public void HeatUp(float heatRate)
    {
        temperature += heatRate;
        Heat_Gauge.value = temperature;
    }
    
    IEnumerator CoolDown()
    {
        while (Heat_Gauge.value != 0)
        {
            Heat_Gauge.value -= cooling * Time.deltaTime;
            overheating = true;
            yield return null;
        }

        overheating = false;
        temperature = 0;
    }
}
