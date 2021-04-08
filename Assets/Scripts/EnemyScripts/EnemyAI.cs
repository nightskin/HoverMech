using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject bullet;
    public Transform muzzle;
    public List<Vector3> patrolPath; //Position of path points
    public bool relative; // Whether or not the path points are relative to the players location
    public bool looping; // If you want the AI to loop through the path
    public float walkSpeed = 3; // Speed when patroling
    public float runSpeed = 4; // Speed when chasing the player
    public Transform target; // What to look for while patroling
    public float atkRange = 6; // how far AI needs to be from target to attack
    public float atkfrequency = 2; // how often the AI attacks the target
    public float aimSpeed = 3; //  how fast the AI aims at the target

    public enum State
    {
        PATROL,
        CHASE,
        ATTACK
    }
    public State state;

    // set to private later
    private int pathIndex;
    private Vector3 originalPos;
    public Vector3 currentDest;
    private float cooldown;

    void Start()
    {
        target = GameObject.Find("Player").transform;
        muzzle = transform.Find("Muzzle");
        if (relative)
        {
            for (int i = 0; i < patrolPath.Count; i++)
            {
                patrolPath[i] += transform.position;
            }
        }
        originalPos = transform.position;
        patrolPath.Add(originalPos);
        pathIndex = 0;
        currentDest = patrolPath[pathIndex];
        cooldown = atkfrequency;
    }

    void Update()
    {
        if (state == State.PATROL)
        {
            Patrol();
        }
        else if(state == State.CHASE)
        {
            Chase();
        }
        else if(state == State.ATTACK)
        {
            Attack();
        }
    }

    void Patrol()
    {
        if (looping)
        {
            PatrolLoop();
        }
        else
        {
            PatrolStop();
        }
    }

    void PatrolLoop()
    {   
        if (pathIndex == patrolPath.Count - 1)
        {
            pathIndex = 0;
        }
        
        if (transform.position != patrolPath[pathIndex])
        {
            float angle = Vector3.Angle(transform.position, patrolPath[pathIndex]);
            transform.LookAt(new Vector3(0,angle,0));
            transform.position = Vector3.MoveTowards(transform.position, patrolPath[pathIndex], walkSpeed * Time.deltaTime);
        }
        else
        {
            pathIndex++;
            currentDest = patrolPath[pathIndex];
        }
    }

    void PatrolStop()
    {
        if (pathIndex < patrolPath.Count)
        {
            if (transform.position != patrolPath[pathIndex])
            {
                float angle = Vector3.Angle(transform.position, patrolPath[pathIndex]);
                transform.LookAt(new Vector3(0, angle, 0));
                transform.position = Vector3.MoveTowards(transform.position, patrolPath[pathIndex], walkSpeed * Time.deltaTime);
            }
            else
            {
                pathIndex++;
                currentDest = patrolPath[pathIndex];
            }
        }
    }

    void Chase()
    {
        transform.LookAt(target);

        float dist = Vector3.Distance(transform.position,target.position);
        if (dist > atkRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, runSpeed * Time.deltaTime);
        }
        else
        {
            state = State.ATTACK;
        }
    }

    void Attack()
    {
        Quaternion desiredRot = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRot, aimSpeed * Time.deltaTime);
        float angle = Vector3.Angle(transform.position + transform.forward, target.position);
        

        cooldown -= Time.deltaTime;
        if(cooldown <= 0)
        {
            if (angle <= 20)
            {
                Fire();
            }
            
        }
    }

    void Fire()
    {
        GameObject b = Instantiate(bullet, muzzle.position, muzzle.rotation);
        b.GetComponent<EnemyAmmo>().direction = muzzle.forward;
        cooldown = atkfrequency;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            state = State.CHASE;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            state = State.PATROL;
        }
    }

}
