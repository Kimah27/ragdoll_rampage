using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoarController : MonoBehaviour
{
    public float growSpeed;

    void Start()
    {
        growSpeed = 12.0f;
        Destroy(gameObject, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = transform.localScale + Vector3.one * growSpeed * Time.deltaTime;
    }
}
