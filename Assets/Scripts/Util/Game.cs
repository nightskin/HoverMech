using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public float worldSize  = 5;

    void Start()
    {
        transform.localScale = new Vector3(worldSize, worldSize, worldSize);
    }

    
    void Update()
    {
        
    }
}
