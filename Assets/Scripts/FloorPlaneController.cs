using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorPlaneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.CompareTag("EnemyProjectile"))
        {
            other.gameObject.transform.Find("Sprite").GetComponent<SpriteRenderer>().enabled = false;
            other.gameObject.transform.Find("Shadow").GetComponent<SpriteRenderer>().enabled = false;
            Destroy(other.gameObject, 1.0f);
		}
	}
}
