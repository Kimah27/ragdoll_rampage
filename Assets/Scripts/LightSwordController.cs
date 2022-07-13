using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwordController : MonoBehaviour
{
    public Vector3 startPosition;
    public Vector3 direction;

    public float speed;
    public float counter;

    public bool isActive;

    void Start()
    {
        startPosition = transform.localPosition;
    }


    void Update()
    {
        if (isActive)
        {
            counter += Time.deltaTime;
            transform.localPosition = transform.localPosition + direction * speed * Time.deltaTime;

            if (counter > 2.0f)
            {
                isActive = false;
                counter = 0.0f;
                transform.localPosition = startPosition;
			}
        }
    }
}
