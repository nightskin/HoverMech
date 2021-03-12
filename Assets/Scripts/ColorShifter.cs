using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorShifter : MonoBehaviour
{

    // Text you want to modulate
    public Text targetTxt;
    // The colors you want to cycle through
    public List<Color> colors;
    // Transition Speed
    public float transitionSpd = 10;

    private int index;


    void Start()
    {
        index = 0;
        targetTxt.color = colors[index + 1];
    }
    
    void Update()
    {
        if(targetTxt.color != colors[index])
        {
            targetTxt.color = Color.Lerp(targetTxt.color, colors[index], transitionSpd * Time.deltaTime);
        }
        else
        {
            if(index >= colors.Count - 1)
            {
                index = 0;
            }
            else
            {
                index++;
            }
        }
    }
}
