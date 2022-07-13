using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleProjectileController : MonoBehaviour
{
    public Transform target;
    public Vector3 direction;
    public float speed;

    void Start()
    {
        speed = 7.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            direction = (target.position - transform.position).normalized;
            transform.position = transform.position + direction * speed * Time.deltaTime;
        }
    }
}
