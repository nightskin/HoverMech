using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    string level;
    public int marker;
    public FinishLine finish;
    public bool reached;

    void Start()
    {
        reached = false;
        finish = GameObject.Find("Finish").GetComponent<FinishLine>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (!reached)
            {
                PlayerPrefs.SetString("Lv", level);
                PlayerPrefs.SetInt("Prog", marker);
                finish.progress.Add(this);
                reached = true;
            }
        }
    }

}
