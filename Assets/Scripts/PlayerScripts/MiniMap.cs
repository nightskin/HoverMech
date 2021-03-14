using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public Camera cam;
    public Transform target;
    public float distance;

    void Start()
    {
        gameObject.SetActive(true);
        cam = transform.GetChild(0).GetComponent<Camera>();
        target = GameObject.Find("Player").transform;
    }

    void LateUpdate()
    {
        cam.transform.position = new Vector3(target.position.x, distance, target.position.z);
    }
}
