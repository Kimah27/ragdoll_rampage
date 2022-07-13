using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileAnimation : MonoBehaviour
{
    public bool destroyFlag;
    public bool isSpinning;
    public bool rotationSet;
    public bool inHolster;

    void Start()
    {
        if (!inHolster)
        {
            Destroy(transform.parent.gameObject, 5.0f);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isSpinning)
        {
            float NewRotation = (480.0f * Time.deltaTime) + gameObject.transform.rotation.eulerAngles.z;
            NewRotation = NewRotation % 360.0f;

            var rot = gameObject.transform.rotation.eulerAngles;
            rot.Set(0.0f, 0.0f, NewRotation);
            gameObject.transform.rotation = Quaternion.Euler(rot);

            
        }
        else if (!rotationSet)
        {
            rotationSet = true;
            Vector3 direction = transform.parent.GetComponent<EnemyAttacks>().direction;
            direction.z = 0.0f;
            float Angle = Vector3.SignedAngle(Vector3.up, direction, new Vector3(0.0f, 0.0f, 1.0f));
            gameObject.transform.eulerAngles = new Vector3(25.0f, 0.0f, Angle);
        }

        if (transform.position.y <= 0.3f)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.transform.parent.transform.Find("Shadow").GetComponent<SpriteRenderer>().enabled = false;
            if (!destroyFlag)
            {
                destroyFlag = true;
                Destroy(transform.parent.gameObject, 1.0f);
            }
        }
    }

	/*private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Environment") || collision.gameObject.CompareTag("FloorPlane"))
        {
            Destroy(gameObject);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.CompareTag("Environment") || other.gameObject.CompareTag("FloorPlane"))
        {
            Destroy(gameObject);
        }
    }*/
}
