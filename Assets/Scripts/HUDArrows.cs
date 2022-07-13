using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDArrows : MonoBehaviour
{
    Vector3 startPos;
    public float timer;
    public float speed;
    public bool isLeft;

    void Start()
    {
        startPos = transform.localPosition;
        timer = 0.0f;
        speed = 15.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < 0.5f)
        {
            timer += Time.deltaTime;
		}
        else if (timer < 0.75f)
        {
            if (!isLeft)
            {
                transform.localPosition = transform.localPosition + Vector3.right * speed * Time.deltaTime;
            }
            else
            {
                transform.localPosition = transform.localPosition + Vector3.left * speed * Time.deltaTime;
            }
            
            timer += Time.deltaTime;
        }
        else if (timer <= 1.0f)
        {
            if (!isLeft)
            {
                transform.localPosition = transform.localPosition + Vector3.left * speed * Time.deltaTime;
            }
            else
            {
                transform.localPosition = transform.localPosition + Vector3.right * speed * Time.deltaTime;
            }

            timer += Time.deltaTime;
        }
        else
        {
            transform.localPosition = startPos;
            timer = 0.0f;
		}
    }
}
