using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceholderAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float NewRotation = (540.0f * Time.deltaTime) + gameObject.transform.rotation.eulerAngles.y;
        NewRotation = NewRotation % 360.0f;

        var rot = gameObject.transform.rotation.eulerAngles;
        rot.Set(0.0f, NewRotation, 0.0f);
        gameObject.transform.rotation = Quaternion.Euler(rot);
    }
}
