using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacoController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-5.0f, 5.0f), 5.0f, Random.Range(-3.0f, 3.0f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void OnCollisionEnter(Collision collision)
	{
		
	}

	public void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerMovement>().Heal(20.0f);
            Destroy(gameObject);
        }
    }
}
