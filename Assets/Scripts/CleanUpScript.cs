using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanUpScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject o in Object.FindObjectsOfType<GameObject>())
        {
            if (o != this)
            {
                Destroy(o);
            }
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
