using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public Transform shadow;
    public RaycastHit hitGround;

    public float distanceToGround;

    public int layerMask;

    void Start()
    {
        shadow = transform.Find("Shadow");
        layerMask = 1 << 13;
    }


    void Update()
    {
        distanceToGround = RaycastDown();
    }

    public float RaycastDown()
    {
        float distance = 10.0f;

        if (Physics.Raycast(transform.position, -Vector3.up, out hitGround, 10.0f, layerMask))
        {
            distance = hitGround.distance;
            Debug.DrawLine(transform.position, hitGround.point, Color.cyan);
        }

        return distance;
    }
}
