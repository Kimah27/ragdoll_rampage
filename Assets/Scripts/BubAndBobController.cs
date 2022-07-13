using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubAndBobController : MonoBehaviour
{
    public GameObject bubbleProjectile;

    public AudioSource audioSource;
    public AudioClip jumpSound;
    public AudioClip bubbleSound;

    public Transform target;
    public Vector3 direction;
    public float speed;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        speed = 7.0f;
        audioSource.PlayOneShot(jumpSound, 0.4f);
        StartCoroutine(Behaviour());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Behaviour()
    {
        yield return new WaitForSeconds(1.5f);

        audioSource.PlayOneShot(bubbleSound, 0.5f);
        var bubble = Instantiate(bubbleProjectile, transform.position, Quaternion.identity);
        bubble.GetComponent<BubbleProjectileController>().target = target;
        transform.Find("Sprite").GetComponent<BubAndBobAnimation>().ChangeSprite();
	}
}
