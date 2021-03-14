using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    public List<GameObject> projectiles;
    public float speed = 5f;
    public float lookSensitivity = 100;
    public float coolDown = 1;
    
    // change to private later
    GameObject equipped;
    float xrot = 0;
    Camera cam;
    Vector2 lookDir;
    float fireDelay;
    PlayerHUD hud;
    Animator anim;
    CharacterController character;

    void Start()
    {
        character = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        hud = GetComponent<PlayerHUD>();
        Cursor.lockState = CursorLockMode.Locked;
        cam = transform.Find("Cam").GetComponent<Camera>();
        equipped = projectiles[0];
        fireDelay = 0;
    }

    void Update()
    {
        if (hud.health > 0)
        {
            Look();
            AirMovment();
            if (Input.GetMouseButton(0))
            {
                Shoot();
            }
        }
        else
        {
            anim.SetTrigger("lose");
        }
    }
    
    void AirMovment()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * y + cam.transform.forward * y;
        character.Move(move * speed * Time.deltaTime);
    }

    void Look()
    {
        lookDir.x = Input.GetAxis("Mouse X") * lookSensitivity * Time.deltaTime;
        lookDir.y = Input.GetAxis("Mouse Y") * lookSensitivity * Time.deltaTime;
        xrot -= lookDir.y;
        xrot = Mathf.Clamp(xrot, -90, 90);

        cam.transform.localRotation = Quaternion.Euler(xrot, 0, 0);
        character.transform.Rotate(Vector3.up * lookDir.x);
    }

    void Shoot()
    {
        if (!hud.overheating)
        {
            fireDelay -= Time.deltaTime;
            if (fireDelay <= 0)
            {
                GameObject b = Instantiate(equipped);
                b.transform.position = cam.transform.position + cam.transform.forward;
                b.GetComponent<Ammo>().direction = Quaternion.Euler(transform.forward) * cam.transform.forward;
                fireDelay = b.GetComponent<Ammo>().fireRate;
                hud.HeatUp(b.GetComponent<Ammo>().cost);
            }
        }
    }

    void Die()
    {
        Cursor.lockState = CursorLockMode.None;
        PlayerPrefs.SetInt("Lv", SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("GameOver");
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Debug.Log("You Are Hit.");
            hud.TakeDamage(other.gameObject.GetComponent<Ammo>().damage);
        }
    }

}
