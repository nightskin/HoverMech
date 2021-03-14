using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public float BaseHP = 30;
    public GameObject poof;
    float HP;

    void Start()
    {
        HP = BaseHP;
    }
    
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            TakeDamage(other.gameObject.GetComponent<Ammo>().damage);
            if (HP <= 0)
            {
                Die();
            }
        }
    }

    void TakeDamage(float dmg)
    {
        HP -= dmg;
        Debug.Log(gameObject.name + " has taken damage");
    }

    void Die()
    {
        Instantiate(poof, transform.position, transform.rotation);
        Destroy(gameObject);
    }


}
