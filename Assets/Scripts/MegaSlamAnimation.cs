using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaSlamAnimation : MonoBehaviour
{
    public float expansionRate;

    void Start()
    {
        expansionRate = 0.2f;
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
