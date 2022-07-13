using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpellController : MonoBehaviour
{
    public GameObject iceSpellHitbox;
    public Rigidbody rb;
    public SphereCollider sc;
    public EnemyAttacks enemyAttacks;
    public Transform target;

    public float lifeCounter;
    public float followTime;
    public float dropTime;
    public float shatterCounter;
    public float shatterTime;

    public bool dropFlag;
    public bool destroyFlag;

    void Start()
    {
        rb = gameObject.GetComponentInParent<Rigidbody>();
        sc = gameObject.GetComponentInParent<SphereCollider>();
        enemyAttacks = gameObject.GetComponent<EnemyAttacks>();
        lifeCounter = 0.0f;
        followTime = 1.0f;
        dropTime = 1.5f;
        shatterCounter = 0.0f;
        shatterTime = 1.2f;
    }


    void Update()
    {
        lifeCounter += Time.deltaTime;

        if (target && lifeCounter < followTime)
        {
            transform.position = target.position + Vector3.up * 2.0f;
		}

        if (lifeCounter >= dropTime && !dropFlag)
        {
            dropFlag = true;
            rb.useGravity = true;
		}

        if (transform.position.y <= 0.6f)
        {
            if (!destroyFlag)
            {
                rb.useGravity = false;
                rb.velocity = Vector3.zero;
                gameObject.transform.Find("Shadow").GetComponent<SpriteRenderer>().enabled = false;
                var hitbox = Instantiate(iceSpellHitbox, transform.position, Quaternion.identity);
                enemyAttacks.PlayHitSound();
                destroyFlag = true;
                Destroy(hitbox, 0.3f);
                Destroy(transform.gameObject, shatterTime);
            }
        }
    }
}
