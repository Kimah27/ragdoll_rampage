using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + Vector3.up * Time.deltaTime;
        transform.localScale = transform.localScale * (1.0f + Time.deltaTime);
    }
}
