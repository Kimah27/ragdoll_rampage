using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacks : MonoBehaviour
{
    public LevelManagement levelManagement;
    public EnemyMovement enemyMovement;
    public EnemyProjectileAnimation enemyProjectileAnimation;
    public ShadowAnimation shadowAnimation;
    public AudioSource castSound;
    public AudioSource hitSound;
    public AudioSource extraSound;

    public Vector3 direction;

    public float damage;
    public float speed;

    public int attackLevel;

    public bool knockback;
    public bool knockup;
    public bool knockdown;
    public bool flinch;
    public bool freeze;
    public bool holsterFireball;

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
        
        if (gameObject.tag != "EnemyProjectile")
        {
            enemyMovement = gameObject.GetComponentInParent<EnemyMovement>();
        }
        else if (transform.Find("Sprite"))
        {
            enemyProjectileAnimation = transform.Find("Sprite").GetComponent<EnemyProjectileAnimation>();
        }

        if (holsterFireball)
        {
            levelManagement = GameObject.Find("LevelManagement").GetComponent<LevelManagement>();
        }
        
    }

    void Update()
    {
        if (direction != Vector3.zero)
        {
            transform.position = transform.position + direction * speed * Time.deltaTime;
            if (holsterFireball)
            {
                transform.position = transform.position + Vector3.right * levelManagement.driftSpeed * Time.deltaTime;

                if (Mathf.Abs(transform.position.x) > 1000.0f || Mathf.Abs(transform.position.y) > 100.0f || Mathf.Abs(transform.position.z) > 50.0f)
                {
                    Destroy(gameObject);
                }
            }
		}
    }

    public void PlayCastSound()
    {
        castSound.Play();
    }

    public void PlayHitSound()
    {
        if (hitSound)
        {
            hitSound.Play();
        }
        
    }

    public void PlayExtraSound()
    {
        extraSound.Play();
    }
}
