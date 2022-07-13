using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KanjiSpin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float NewRotation = (180.0f * Time.deltaTime) + gameObject.transform.rotation.eulerAngles.z;
        NewRotation = NewRotation % 360.0f;

        var rot = gameObject.transform.rotation.eulerAngles;
        rot.Set(0.0f, 0.0f, NewRotation);
        gameObject.transform.rotation = Quaternion.Euler(rot);
    }
}
