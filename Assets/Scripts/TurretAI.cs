using UnityEngine;

public class TurretAI : MonoBehaviour
{
    public float fireRate = 2;
    public float aimSpeed = 25;
    public GameObject projectile;
    public float hp = 25;

    Transform muzzle;
    Transform target;
    bool targetDetected;
    float cooldown;

    void Start()
    {
        muzzle = transform.GetChild(0);
        target = GameObject.Find("Player").transform;
        targetDetected = false;
        cooldown = 0;
    }

    void Update()
    {
        if(targetDetected)
        {
            Aim();
        }
        
        if(hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Aim()
    {
        Quaternion desiredRot = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRot, aimSpeed * Time.deltaTime);
        float angle = Vector3.Angle(muzzle.position, target.position);
        
        if(angle <= 20)
        {
            Fire();
        }
    }

    void Fire()
    {
        cooldown -= Time.deltaTime;
        if(cooldown <= 0)
        {
            GameObject b = Instantiate(projectile);
            b.transform.position = muzzle.position;
            b.GetComponent<Ammo>().transform.rotation = transform.rotation;
            b.GetComponent<Ammo>().direction = transform.forward;
            cooldown = fireRate;
        }
    }

    public void TakeDamage(float dmg)
    {
        hp -= dmg;
        Debug.Log("Turret took damage");
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            TakeDamage(other.gameObject.GetComponent<Ammo>().damage);
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            targetDetected = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            targetDetected = false;
        }
    }

}
