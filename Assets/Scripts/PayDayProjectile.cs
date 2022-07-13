using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayDayProjectile : MonoBehaviour
{
    public PlayerAttacks playerAttacks;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(transform.position.x) > 1000.0f || Mathf.Abs(transform.position.y) > 100.0f || Mathf.Abs(transform.position.z) > 50.0f)
        {
            Destroy(gameObject);
		}
    }
}
