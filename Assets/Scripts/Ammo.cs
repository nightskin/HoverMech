using UnityEngine;

public class Ammo : MonoBehaviour
{
    public float speed = 10;
    public Vector3 direction;
    public float fireRate = 0.5f;

    public float damage = 10;
    public float cost = 5;

    public bool explosive;
    public float blastRadius = 3;
    
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Bounds")
        {
            Destroy(gameObject);
        }
    }

}
