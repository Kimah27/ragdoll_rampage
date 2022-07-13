using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPoundAnimation : MonoBehaviour
{
    public float expansionRate;

    void Start()
    {
        expansionRate = 0.5f;
    }


    void Update()
    {

    }

    void FixedUpdate()
    {
        ExpandShockwave();
    }

    public void ExpandShockwave()
    {
        transform.localScale = transform.localScale + Vector3.one * expansionRate * Time.deltaTime;
    }
}
