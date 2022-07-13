using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowlAnimation : MonoBehaviour
{
    public List<Sprite> sprites;

    public Vector3 trajectory;

    public float moveSpeed;
    public float counter;
    public float duration;

    public int index;

    void Start()
    {
        moveSpeed = 4.0f;
        counter = 0.0f;
        duration = 2.5f;
        index = Random.Range(0, 2);

        if (GetComponent<SpriteRenderer>())
        {
            GetComponent<SpriteRenderer>().sprite = sprites[index];
        }
    }

    
    void Update()
    {
        counter += Time.deltaTime;

        if (counter >= duration)
        {
            Destroy(gameObject);
		}
    }

	void FixedUpdate()
	{
        MoveProjectile();
        RotateProjectile();
    }

    public void MoveProjectile()
    {
        if (gameObject.GetComponent<SphereCollider>())
        {
            transform.position = transform.position + trajectory * moveSpeed * Time.deltaTime;
        }
            
    }

    public void RotateProjectile()
    {
        if (gameObject.GetComponent<SpriteRenderer>())
        {
            float NewRotation = (360.0f * Time.deltaTime) + gameObject.transform.rotation.eulerAngles.z;
            NewRotation = NewRotation % 360.0f;

            var rot = gameObject.transform.rotation.eulerAngles;
            rot.Set(0.0f, 0.0f, NewRotation);
            gameObject.transform.rotation = Quaternion.Euler(rot);
        }
        
    }
}
