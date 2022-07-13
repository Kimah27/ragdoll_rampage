using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyText : MonoBehaviour
{
    public float lifeTime;
    public float counter;
    public float speed;
    public Vector3 offset;
    public Vector3 direction;
    public Vector2 randomiser;

    void Start()
    {
        lifeTime = 1.0f;
        counter = 0.0f;
        speed = 2.0f;
        offset = new Vector3(0.0f, 0.6f, 0.0f);
        randomiser = new Vector2(-0.5f, 0.5f);
        transform.localPosition += offset;
        direction = new Vector3(Random.Range(randomiser.x, randomiser.y), 1.5f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        transform.localPosition += direction * speed * Time.deltaTime;

        if (counter >= lifeTime)
        {
            Destroy(gameObject);
		}
    }
}
