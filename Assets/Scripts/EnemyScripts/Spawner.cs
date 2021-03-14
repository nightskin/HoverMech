using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform Player;
    public GameObject[] EnemyTypes;
    public int MaxEnemies = 10;
    public int NumEnemies;
    public float minOffset = 6;
    public float maxOffset = 15;

    public float interval = 5;
    float delay;
    

    void Start()
    {
        Player = GameObject.Find("Player").transform;
        delay = 0;
        NumEnemies = 0;
    }
    
    void Update()
    {
        delay -= Time.deltaTime;
        if(delay <= 0)
        {
            Spawn();
            delay = interval;
        }
    }

    void Spawn()
    {
        if (NumEnemies < MaxEnemies)
        {
            int e = Random.Range(0, EnemyTypes.Length);
            float offset = Random.Range(minOffset, maxOffset);
            Vector3 pos = new Vector3(Player.position.x + offset, 10, Player.position.z + offset);
            Instantiate(EnemyTypes[e],pos, new Quaternion(0,0,0,1));
            NumEnemies++;
        }
    }

}
