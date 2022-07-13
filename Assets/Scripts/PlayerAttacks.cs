using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public AudioSource castSound;
    public AudioSource hitSound;
    public AudioSource extraSound;

    public Vector3 direction;
    public float speed;
    public float damage;
    public float stun;

    public int attackLevel;

    public bool knockback;
    public bool knockup;
    public bool knockdown;

	private void Awake()
	{
        AudioSource[] sources = GetComponents<AudioSource>();
        
        castSound = sources[0];
        hitSound = sources[1];
        if (sources.Length > 2)
        {
            extraSound = sources[2];
        }
    }
	void Start()
    {
        if (gameObject.tag != "PlayerProjectile")
        {
            playerMovement = gameObject.GetComponentInParent<PlayerMovement>();
        }
    }


    void Update()
    {
        if (direction != Vector3.zero)
        {
            transform.position = transform.position + direction * speed * Time.deltaTime;
		}
    }

    public void PlayCastSound()
    {
        castSound.Play();
    }

    public void PlayHitSound()
    {
        hitSound.Play();
	}

    public void PlayExtraSound()
    {
        extraSound.Play();
    }
}
