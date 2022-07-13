using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleAttackController : MonoBehaviour
{
    public BoxCollider bc;

    public float lifeCounter;
    public float lifeTime;
    public float hitboxTime;

    void Start()
    {
        bc = gameObject.GetComponent<BoxCollider>();
        lifeCounter = 0.0f;
        lifeTime = 1.5f;
        hitboxTime = 0.45f;
    }

    
    void Update()
    {
        if (lifeCounter >= hitboxTime && bc.enabled)
        {
            bc.enabled = false;
		}
        if (lifeCounter >= lifeTime)
        {
            Destroy(gameObject);
		}

        lifeCounter += Time.deltaTime;
    }
}
